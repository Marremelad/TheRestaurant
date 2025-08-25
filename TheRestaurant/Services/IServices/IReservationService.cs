using TheRestaurant.DTOs;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface IReservationService
{
    Task<ServiceResponse<IEnumerable<ReservationDto>>> GetReservationsAsync();

    Task<ServiceResponse<ReservationDto>> GetReservationAsync(string reservationEmail);
    
    Task<ServiceResponse<Unit>> CreateReservationAsync(ReservationDto reservationDto);

    Task<ServiceResponse<Unit>> DeleteReservationAsync(string reservationEmail);
}