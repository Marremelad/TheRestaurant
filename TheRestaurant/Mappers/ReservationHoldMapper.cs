using TheRestaurant.DTOs;
using TheRestaurant.Models;

namespace TheRestaurant.Mappers;

public static class ReservationHoldMapper
{
    public static ReservationHoldDto ToDto(ReservationHold reservationHold) =>
        new(
            reservationHold.Date,
            reservationHold.TimeSlot,
            reservationHold.TableNumber,
            reservationHold.TableCapacity
        );

    public static List<ReservationHoldDto> ToDtos(IEnumerable<ReservationHold> reservationHolds) =>
        reservationHolds.Select(ToDto).ToList();
    
    public static ReservationHold FromAvailabilityResponseDto(AvailabilityResponseDto availabilityResponseDto) =>
        new()
        {
            Date = availabilityResponseDto.Date,
            TimeSlot = availabilityResponseDto.TimeSlot,
            TableNumber = availabilityResponseDto.TableNumber,
            TableCapacity = availabilityResponseDto.TableCapacity
        };
}