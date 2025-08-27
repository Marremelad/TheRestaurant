using System.Runtime.InteropServices.JavaScript;
using TheRestaurant.Enums;

namespace TheRestaurant.DTOs;

public record AvailabilityResponseDto(
    DateOnly Date,
    TimeSlot TimeSlot,
    int TableNumber,
    int TableCapacity
    );