using System.ComponentModel.DataAnnotations;
using TheRestaurant.Enums;

namespace TheRestaurant.DTOs;

public record ReservationDto(
    DateOnly Date,
    TimeSlot TimeSlot,
    int TableNumber,
    string FirstName,
    string LastName,
    string Email
    );