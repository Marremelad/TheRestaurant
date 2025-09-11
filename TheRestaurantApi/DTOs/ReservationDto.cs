using System.ComponentModel.DataAnnotations;
using TheRestaurant.Enums;

namespace TheRestaurant.DTOs;

public record ReservationDto(
    int Id,
    DateOnly Date,
    string DisplayableTimeSlot,
    int TableNumber,
    string FirstName,
    string LastName,
    string Email
    );