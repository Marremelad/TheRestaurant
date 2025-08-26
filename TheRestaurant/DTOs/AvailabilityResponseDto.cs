using System.Runtime.InteropServices.JavaScript;
using TheRestaurant.Enums;

namespace TheRestaurant.DTOs;

public record AvailabilityResponseDto(
    int TableNumber,
    TimeSlot TimeSlot,
    DateOnly Date,
    int TableCapacity
    );