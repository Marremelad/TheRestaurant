using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

public class ReservationService(
    IReservationRepository reservationRepository,
    ITableRepository tableRepository,
    ILogger<ReservationService> logger
    ) : IReservationService
{
    public async Task<ServiceResponse<List<ReservationDto>>> GetReservationsAsync()
    {
        try
        {
            var reservations = await reservationRepository.GetReservationsAsync();

            return ServiceResponse<List<ReservationDto>>.Success(
                HttpStatusCode.OK,
                ReservationMapper.ToDtos(reservations),
                "Reservations fetched successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch reservations.";
            logger.LogError(ex, message);
            return ServiceResponse<List<ReservationDto>>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }

    public async Task<ServiceResponse<List<ReservationDto>>> GetReservationsByEmailAsync(string reservationEmail)
    {
        try
        {
            var reservations = await reservationRepository.GetReservationsByEmailAsync(reservationEmail);

            if (reservations.Count == 0)
                return ServiceResponse<List<ReservationDto>>.Failure(
                    HttpStatusCode.NotFound,
                    $"Reservation associated with the email ({reservationEmail}) does not exist."
                );
        
            return ServiceResponse<List<ReservationDto>>.Success(
                HttpStatusCode.OK,
                ReservationMapper.ToDtos(reservations),
                "Reservation fetched successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch a reservation";
            logger.LogError(ex, message);
            return ServiceResponse<List<ReservationDto>>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
        
    }

    public async Task<ServiceResponse<Unit>> CreateReservationAsync(ReservationDto reservationDto)
    {
        try
        {
            if (await tableRepository.GetTableByTableNumberAsync(reservationDto.TableNumber) == null)
                return ServiceResponse<Unit>.Failure(
                    HttpStatusCode.NotFound,
                    $"Table number ({reservationDto.TableNumber}) does not exist."
                );
            
            var reservation = ReservationMapper.ToEntity(reservationDto);

            return ServiceResponse<Unit>.Success(
                HttpStatusCode.Created,
                await reservationRepository.CreateReservationAsync(reservation),
                "The reservation was created successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to create a reservation.";
            logger.LogError(ex, message);
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }

    public async Task<ServiceResponse<Unit>> DeleteReservationAsync(string reservationEmail)
    {
        try
        {
            var reservations = await reservationRepository.GetReservationsByEmailAsync(reservationEmail);

            if (reservations.Count == 0)
                return ServiceResponse<Unit>.Failure(
                    HttpStatusCode.NotFound,
                    $"Reservation associated with the email ({reservationEmail}) does not exist."
                );

            return ServiceResponse<Unit>.Success(
                HttpStatusCode.OK,
                await reservationRepository.DeleteReservationsAsync(reservations),
                "Reservations deleted successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to delete a reservation.";
            logger.LogError(ex, message);
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
            $"{message}: {ex.Message}"
            );
        }
    }
}