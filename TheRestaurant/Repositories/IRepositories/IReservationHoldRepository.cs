using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories.IRepositories;

public interface IReservationHoldRepository
{
    Task<ReservationHold?> GetReservationHoldByIdAsync(int reservationHoldId);
    
    Task<List<ReservationHold>> GetReservationHoldsAsync();

    Task<int> CreateReservationHoldAsync(ReservationHold reservationHold);

    Task<Unit> DeleteReservationHoldAsync(ReservationHold reservationHold);
}