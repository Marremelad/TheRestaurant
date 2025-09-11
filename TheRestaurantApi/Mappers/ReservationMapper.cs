using TheRestaurant.DTOs;
using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Mappers;

public static class ReservationMapper
{
    public static ReservationDto ToDto(Reservation reservation)
    {
        var timeSlot = TimeSlotExtensions.TimeSlotMappings[reservation.TimeSlot];
        var display = $@"{timeSlot.Start:hh\:mm} - {timeSlot.End:hh\:mm}";
        
        return new ReservationDto(
            reservation.Id,
            reservation.Date,
            display,
            reservation.TableNumber,
            reservation.FirstName,
            reservation.LastName,
            reservation.Email
        );
    }
    
    public static List<ReservationDto> ToDtos(IEnumerable<Reservation> reservations) =>
        reservations.Select(ToDto).ToList();
}