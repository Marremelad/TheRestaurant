using System.Net;
using Microsoft.EntityFrameworkCore;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

/// <summary>
/// Manages temporary reservation holds during the booking process to prevent double-booking race conditions.
/// </summary>
public class ReservationHoldService(
    IReservationHoldRepository reservationHoldRepository,
    ILogger<ReservationHoldService> logger
    ) : IReservationHoldService
{
    /// <summary>
    /// Retrieves all currently held reservations for administrative monitoring purposes.
    /// </summary>
    public async Task<ServiceResponse<List<AvailabilityResponseDto>>> GetReservationHoldsAsync()
    {
        try
        {
            var reservationHolds = await reservationHoldRepository.GetReservationHoldsAsync();

            return ServiceResponse<List<AvailabilityResponseDto>>.Success(
                HttpStatusCode.OK,
                ReservationHoldMapper.ToDtos(reservationHolds),
                "Held reservations fetched successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch held reservations.";
            logger.LogError(ex, message);
            return ServiceResponse<List<AvailabilityResponseDto>>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }

    /// <summary>
    /// Creates a temporary hold on a table/timeslot combination to reserve it during the booking process.
    /// </summary>
    public async Task<ServiceResponse<int>> CreateReservationHoldAsync(AvailabilityResponseDto availabilityResponseDto)
    {
        try
        {
            // Convert availability response to reservation hold entity for database storage.
            var reservationHold = ReservationHoldMapper.FromAvailabilityResponseDto(availabilityResponseDto);

            return ServiceResponse<int>.Success(
                HttpStatusCode.Created,
                await reservationHoldRepository.CreateReservationHoldAsync(reservationHold),
                "Reservation is being held successfully."
            );
        }
        catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
        {
            // Handle race condition when multiple users try to hold the same table/timeslot simultaneously.
            return ServiceResponse<int>.Failure(
                HttpStatusCode.Conflict,
                "This time slot was just taken by another customer. Please select a different option."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred. Could not hold reservation.";
            logger.LogError(ex, message);
            return ServiceResponse<int>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }
    
    /// <summary>
    /// Detects unique constraint violations in database exceptions to identify concurrent booking attempts.
    /// </summary>
    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        return ex.InnerException?.Message.Contains("unique constraint") == true ||
               ex.InnerException?.Message.Contains("duplicate key") == true;
    }
}