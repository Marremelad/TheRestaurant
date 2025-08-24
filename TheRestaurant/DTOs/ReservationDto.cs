namespace TheRestaurant.DTOs;

public record ReservationDto(
    DateOnly Date,
    int TimeSlot,
    int TableNumber,
    string FirstName,
    string LastName,
    string Email
    );