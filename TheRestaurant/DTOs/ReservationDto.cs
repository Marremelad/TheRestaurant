using System.ComponentModel.DataAnnotations;
using TheRestaurant.Attributes;
using TheRestaurant.Enums;

namespace TheRestaurant.DTOs;

public record ReservationDto(
    DateOnly Date,
    [EnumDataType(typeof(TimeSlot))] TimeSlot TimeSlot,
    [ValidTableNumber] int TableNumber,
    [StringLength(50)] string FirstName,
    [StringLength(50)] string LastName,
    [EmailAddress] string Email
    );