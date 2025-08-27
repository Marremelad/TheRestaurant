using TheRestaurant.DTOs;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface IReservationService
{
    Task<ServiceResponse<List<ReservationDto>>> GetReservationsAsync();

    Task<ServiceResponse<List<ReservationDto>>> GetReservationsByEmailAsync(string reservationEmail);
    
    Task<ServiceResponse<Unit>> CreateReservationAsync(ReservationDto reservationDto);

    Task<ServiceResponse<Unit>> DeleteReservationAsync(string reservationEmail);
}