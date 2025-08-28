using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

public class ReservationService(
    IReservationRepository reservationRepository,
    IReservationHoldRepository reservationHoldRepository,
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

    public async Task<ServiceResponse<Unit>> CreateReservationAsync(PersonalInfoDto personalInfoDto, int reservationHoldId)
    {
        try
        {
            var reservationHold = await reservationHoldRepository.GetReservationHoldByIdAsync(reservationHoldId);

            if (reservationHold == null)
                return ServiceResponse<Unit>.Failure(
                    HttpStatusCode.NotFound,
                    $"The held reservations id ({reservationHoldId}) does not match any held reservations in the database."
                );

            var reservation = new Reservation()
            {
                Date = reservationHold.Date,
                TimeSlot = reservationHold.TimeSlot,
                TableNumber = reservationHold.TableNumber,
                FirstName = personalInfoDto.FirstName,
                LastName = personalInfoDto.LastName,
                Email = personalInfoDto.Email
            };

            var repositoryResponse = await reservationRepository.CreateReservationAsync(reservation);
            await reservationHoldRepository.DeleteReservationHoldAsync(reservationHold);
            
            return ServiceResponse<Unit>.Success(
                HttpStatusCode.Created,
                repositoryResponse,
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

    public async Task<ServiceResponse<Unit>> DeleteReservationsAsync(string reservationEmail)
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