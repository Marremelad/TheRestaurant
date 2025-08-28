using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Models;
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
                message
            );
        }
    }

    public async Task<ServiceResponse<int>> CreateReservationHoldAsync(AvailabilityResponseDto availabilityResponseDto)
    {
        try
        {
            var reservationHold = ReservationHoldMapper.FromAvailabilityResponseDto(availabilityResponseDto);

            return ServiceResponse<int>.Success(
                HttpStatusCode.OK,
                await reservationHoldRepository.CreateReservationHoldAsync(reservationHold),
                "Reservation is being held successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred. Could not hold reservation.";
            logger.LogError(ex, message);
            return ServiceResponse<int>.Failure(
                HttpStatusCode.InternalServerError,
                ex.Message
            );
        }
    }
}