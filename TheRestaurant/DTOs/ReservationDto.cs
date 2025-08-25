using System.ComponentModel.DataAnnotations;

namespace TheRestaurant.DTOs;

public record ReservationDto(
    DateOnly Date,
    int TimeSlot,
    int TableNumber,
    [StringLength(50)] string FirstName,
    [StringLength(50)] string LastName,
    [EmailAddress] string Email
    );