using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Enums;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

public class AvailabilityService(
    IReservationRepository repository,
    DbSetTracker tracker,
    ILogger<AvailabilityService> logger
    ) : IAvailabilityService
{
    public async Task<ServiceResponse<IEnumerable<AvailabilityResponseDto>>>
        GetAvailableTablesByCustomerInputAsync(AvailabilityRequestDto availabilityRequestDto)
    {
        try
        {
            var allCombinations = tracker.Tables
                .Where(table => table.Capacity >= availabilityRequestDto.PartySize)
                .SelectMany(table => Enum.GetValues<TimeSlot>()
                    .Select(timeSlot => new AvailabilityResponseDto
                    (
                        table.Number,
                        timeSlot,
                        availabilityRequestDto.Date,
                        table.Capacity
                    )));

            var reservedCombinations = (await repository.GetReservationsByDate(availabilityRequestDto.Date))
                .Where(reservation => reservation.Table!.Capacity >= availabilityRequestDto.PartySize)
                .Select(reservation => new AvailabilityResponseDto
                (
                    reservation.TableNumber,
                    reservation.TimeSlot,
                    reservation.Date,
                    reservation.Table!.Capacity
                ));


            var availableCombinations = allCombinations.Except(reservedCombinations)
                .GroupBy(combinations => combinations.TimeSlot)
                .Select(group => group
                    .OrderBy(combination => combination.TableCapacity)
                    .First())
                .ToList();

            return ServiceResponse<IEnumerable<AvailabilityResponseDto>>.Success(
                HttpStatusCode.OK,
                availableCombinations,
                availableCombinations.Count == 0
                    ? "No available table for the requested criteria."
                    : "Available combinations of table + time slots fetched successfully.");
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch available table + time slot combinations.";
            logger.LogError(ex, message);
            return ServiceResponse<IEnumerable<AvailabilityResponseDto>>.Failure(
                HttpStatusCode.InternalServerError,
                message
                );
        }
    }
}