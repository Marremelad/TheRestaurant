namespace TheRestaurant.DTOs;

// public record ReservationCreateDto(
//     int ReservationHoldId,
//     PersonalInfoDto PersonalInfo
//     );

public class ReservationCreateDto
{
    public int ReservationHoldId { get; init; }
    
    public required PersonalInfoDto PersonalInfo { get; init; }
}