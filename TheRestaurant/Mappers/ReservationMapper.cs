using TheRestaurant.DTOs;
using TheRestaurant.Models;

namespace TheRestaurant.Mappers;

public static class ReservationMapper
{
    public static ReservationDto ToDto(Reservation reservation) =>
        new(reservation.Date,
            reservation.TimeSlot,
            reservation.TableNumber,
            reservation.FirstName,
            reservation.LastName,
            reservation.Email);
    
    public static IEnumerable<ReservationDto> ToDtos(IEnumerable<Reservation> reservations) =>
        reservations.Select(ToDto);

    public static Reservation ToEntity(ReservationDto reservationDto) =>
        new()
        {
            Date = reservationDto.Date,
            TimeSlot = reservationDto.TimeSlot,
            TableNumber = reservationDto.TableNumber,
            FirstName = reservationDto.FirstName,
            LastName = reservationDto.LastName,
            Email = reservationDto.Email
        };

    public static IEnumerable<Reservation> ToEntities(IEnumerable<ReservationDto> reservationDtos) =>
        reservationDtos.Select(ToEntity);
}