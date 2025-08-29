using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

/// <summary>
/// Handles table availability checking by filtering out reserved and held tables for specific dates and time slots.
/// </summary>
public class AvailabilityService(
    ITableRepository tableRepository,
    IReservationRepository reservationRepository,
    IReservationHoldRepository reservationHoldRepository,
    ILogger<AvailabilityService> logger
    ) : IAvailabilityService
{
    /// <summary>
    /// Finds all available table and time slot combinations that can accommodate the requested party size on the specified date.
    /// </summary>
    public async Task<ServiceResponse<IEnumerable<AvailabilityResponseDto>>>
        GetAvailableTablesByCustomerInputAsync(AvailabilityRequestDto availabilityRequestDto)
    {
        try
        {
            // Get valid time slots for the requested date (excludes time slots in the past for today).
            var validTimeslots = TimeSlotExtensions.GetValidTimeSlotsForDate(availabilityRequestDto.Date);
            
            // Create all possible combinations of tables (with sufficient capacity) and valid time slots.
            var validCombinations = (await tableRepository.GetTablesAsync())
                .Where(table => table.Capacity >= availabilityRequestDto.PartySize)
                .SelectMany(table => validTimeslots
                    .Select(timeSlot => new AvailabilityResponseDto
                    (
                        availabilityRequestDto.Date,
                        timeSlot,
                        table.Number,
                        table.Capacity
                    )));

            // Get all confirmed reservations for the requested date that could accommodate the party size.
            var reservedCombinations = (await reservationRepository.GetReservationsByDateAsync(availabilityRequestDto.Date))
                .Where(reservation => reservation.Table!.Capacity >= availabilityRequestDto.PartySize)
                .Select(reservation => new AvailabilityResponseDto
                (
                    reservation.Date,
                    reservation.TimeSlot,
                    reservation.TableNumber,
                    reservation.Table!.Capacity
                ));

            // Get all temporarily held reservations (tables being held during booking process).
            var reservedHoldCombinations = (await reservationHoldRepository.GetReservationHoldsAsync())
                .Select(reservedHold => new AvailabilityResponseDto(
                    reservedHold.Date,
                    reservedHold.TimeSlot,
                    reservedHold.TableNumber,
                    reservedHold.TableCapacity
                ));

            // Combine both reserved and held tables as unavailable options.
            var unavailableCombinations = reservedCombinations.Concat(reservedHoldCombinations);

            // Filter out unavailable combinations and select the smallest suitable table for each time slot.
            var availableCombinations = validCombinations.Except(unavailableCombinations)
                .GroupBy(combinations => combinations.TimeSlot)
                .Select(group => group
                    .OrderBy(combination => combination.TableCapacity) // Prioritize smaller tables to optimize seating.
                    .First())
                .ToList();

            return ServiceResponse<IEnumerable<AvailabilityResponseDto>>.Success(
                HttpStatusCode.OK,
                availableCombinations,
                availableCombinations.Count == 0
                    ? "No available table for the requested criteria."
                    : "Available combinations of table + time slots fetched successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch available table + time slot combinations.";
            logger.LogError(ex, message);
            return ServiceResponse<IEnumerable<AvailabilityResponseDto>>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }
}