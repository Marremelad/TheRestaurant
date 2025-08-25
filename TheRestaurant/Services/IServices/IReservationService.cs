using TheRestaurant.DTOs;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface IReservationService
{
    Task<ServiceResponse<Unit>> CreateReservationAsync(ReservationDto reservationDto);
}