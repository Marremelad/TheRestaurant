namespace TheRestaurant.DTOs;

public record ReservationDto(
    DateOnly Date,
    int TimeSlot,
    int TableId,
    string FirstName,
    string LastName,
    string Email
    );