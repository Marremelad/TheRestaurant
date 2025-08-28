using TheRestaurant.Enums;

namespace TheRestaurant.DTOs;

public record ReservationHoldDto(
    DateOnly Date,
    TimeSlot TimeSlot,
    int TableNumber,
    int TableCapacity
    );