namespace TheRestaurant.DTOs;

public record AvailabilityResponseDto(
    DateOnly Date,
    string TimeSlot,
    int TableNumber,
    int TableCapacity 
    );