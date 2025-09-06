using TheRestaurant.DTOs;
using TheRestaurant.Models;

namespace TheRestaurant.Mappers;

public static class ReservationHoldMapper
{
    public static AvailabilityProcessorDto ToDto(ReservationHold reservationHold) =>
        new(
            reservationHold.Date,
            reservationHold.TimeSlot,
            reservationHold.TableNumber,
            reservationHold.TableCapacity
        );

    public static List<AvailabilityProcessorDto> ToDtos(IEnumerable<ReservationHold> reservationHolds) =>
        reservationHolds.Select(ToDto).ToList();
    
    public static ReservationHold FromAvailabilityResponseDto(AvailabilityProcessorDto availabilityProcessorDto) =>
        new()
        {
            Date = availabilityProcessorDto.Date,
            TimeSlot = availabilityProcessorDto.TimeSlot,
            TableNumber = availabilityProcessorDto.TableNumber,
            TableCapacity = availabilityProcessorDto.TableCapacity
        };
}