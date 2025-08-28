using TheRestaurant.DTOs;
using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface IReservationHoldService
{
    Task<ServiceResponse<List<ReservationHoldDto>>> GetReservationHoldsAsync();
    
    Task<ServiceResponse<int>> CreateReservationHoldAsync(AvailabilityResponseDto availabilityResponseDto);
}