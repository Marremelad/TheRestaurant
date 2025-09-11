using TheRestaurant.DTOs;
using TheRestaurant.Models;

namespace TheRestaurant.Mappers;

public static class ReservationMapper
{
    public static ReservationDto ToDto(Reservation reservation) =>
        new(
            reservation.Id,
            reservation.Date,
            reservation.TimeSlot,
            reservation.TableNumber,
            reservation.FirstName,
            reservation.LastName,
            reservation.Email
        );
    
    public static List<ReservationDto> ToDtos(IEnumerable<Reservation> reservations) =>
        reservations.Select(ToDto).ToList();
}