using System.Net;
using Microsoft.EntityFrameworkCore;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

public class ReservationHoldService(
    IReservationHoldRepository reservationHoldRepository,
    ILogger<ReservationHoldService> logger
    ) : IReservationHoldService
{
    public async Task<ServiceResponse<List<ReservationHoldDto>>> GetReservationHoldsAsync()
    {
        try
        {
            var reservationHolds = await reservationHoldRepository.GetReservationHoldsAsync();

            return ServiceResponse<List<ReservationHoldDto>>.Success(
                HttpStatusCode.OK,
                ReservationHoldMapper.ToDtos(reservationHolds),
                "Held reservations fetched successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch held reservations.";
            logger.LogError(ex, message);
            return ServiceResponse<List<ReservationHoldDto>>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }

    public async Task<ServiceResponse<int>> CreateReservationHoldAsync(AvailabilityResponseDto availabilityResponseDto)
    {
        try
        {
            var reservationHold = ReservationHoldMapper.FromAvailabilityResponseDto(availabilityResponseDto);

            return ServiceResponse<int>.Success(
                HttpStatusCode.Created,
                await reservationHoldRepository.CreateReservationHoldAsync(reservationHold),
                "Reservation is being held successfully."
            );
        }
        catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
        {
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
    
    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        return ex.InnerException?.Message.Contains("unique constraint") == true ||
               ex.InnerException?.Message.Contains("duplicate key") == true;
    }
}