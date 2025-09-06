using TheRestaurant.Enums;

namespace TheRestaurant.DTOs;

public record AvailabilityProcessorDto(
    DateOnly Date,
    TimeSlot TimeSlot,
    int TableNumber,
    int TableCapacity
    );