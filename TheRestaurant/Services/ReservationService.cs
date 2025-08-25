using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

public class ReservationService(
    IReservationRepository repository,
    ILogger<ReservationService> logger
    ) : IReservationService
{
    public async Task<ServiceResponse<IEnumerable<ReservationDto>>> GetReservationsAsync()
    {
        try
        {
            var reservations = await repository.GetReservationsAsync();

            return ServiceResponse<IEnumerable<ReservationDto>>.Success(
                HttpStatusCode.OK,
                ReservationMapper.ToDtos(reservations),
                "Reservations fetched successfully."
                );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch reservations.";
            logger.LogError(ex, message);
            return ServiceResponse<IEnumerable<ReservationDto>>.Failure(
                HttpStatusCode.InternalServerError,
                message
                );
        }
    }

    public async Task<ServiceResponse<ReservationDto>> GetReservationAsync(string reservationEmail)
    {
        try
        {
            var reservation = await repository.GetReservationAsync(reservationEmail);
        
            if (reservation == null)
                return ServiceResponse<ReservationDto>.Failure(
                    HttpStatusCode.NotFound,
                    $"Reservation associated with the email ({reservationEmail}) does not exist."
                );
        
            return ServiceResponse<ReservationDto>.Success(
                HttpStatusCode.OK,
                ReservationMapper.ToDto(reservation),
                "Reservation fetched successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch a reservation";
            logger.LogError(ex, message);
            return ServiceResponse<ReservationDto>.Failure(
                HttpStatusCode.InternalServerError,
                message
                );
        }
        
    }

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
            const string message = "An error occurred while trying to create a reservation.";
            logger.LogError(ex, message);
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
                message
                );
        }
    }

    public async Task<ServiceResponse<Unit>> DeleteReservationAsync(string reservationEmail)
    {
        try
        {
            var reservation = await repository.GetReservationAsync(reservationEmail);
            
            if (reservation == null)
                return ServiceResponse<Unit>.Failure(
                    HttpStatusCode.NotFound,
                    $"Reservation associated with the email ({reservationEmail}) does not exist."
                    );
            
            return ServiceResponse<Unit>.Success(
                HttpStatusCode.OK,
                await repository.DeleteReservationAsync(reservation),
                "Reservation deleted successfully."
                );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to delete a reservation.";
            logger.LogError(ex, message);
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
                message
                );
        }
    }
}