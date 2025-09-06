namespace TheRestaurant.DTOs;

public class ReservationCreateDto
{
    public int ReservationHoldId { get; init; }
    
    public required PersonalInfoDto PersonalInfo { get; init; }
}