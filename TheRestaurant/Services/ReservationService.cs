using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

public class ReservationService(
    IReservationRepository repository,
    ILogger logger
    ) : IReservationService
{
    public async Task<ServiceResponse<Unit>> CreateReservationAsync(ReservationDto reservationDto)
    {
        try
        {
            var reservation = ReservationMapper.ToEntity(reservationDto);

            return ServiceResponse<Unit>.Success(
                HttpStatusCode.Created,
                await repository.CreateReservationAsync(reservation),
                "The reservation was created successfully."
                );
        }
        catch (Exception ex)
        {
            const string errorMessage = "An error occurred while trying to create a reservation.";
            logger.LogError(errorMessage);
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
                errorMessage);
        }
        
    }
}