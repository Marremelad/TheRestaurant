using System.ComponentModel.DataAnnotations;
using TheRestaurant.Attributes;

namespace TheRestaurant.DTOs;

public record ReservationDto(
    DateOnly Date,
    [ValidTimeSlot] int TimeSlot,
    [ValidTableNumber] int TableNumber,
    [StringLength(50)] string FirstName,
    [StringLength(50)] string LastName,
    [EmailAddress] string Email
    );