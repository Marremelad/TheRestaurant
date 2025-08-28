namespace TheRestaurant.DTOs;

public record ReservationCreateDto(
    int ReservationHoldId,
    PersonalInfoDto PersonalInfo
    );