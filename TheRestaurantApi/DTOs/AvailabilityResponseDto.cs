using TheRestaurant.Enums;

namespace TheRestaurant.DTOs;

public record AvailabilityResponseDto(
    DateOnly Date,
    TimeSlot TimeSlot,
    string DisplayableTimeSlot,
    int TableNumber,
    int TableCapacity 
    );