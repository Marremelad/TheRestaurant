using TheRestaurant.Models;

namespace TheRestaurant.Repositories.IRepositories;

public interface IReservationHoldRepository
{
    Task<List<ReservationHold>> GetReservationHoldsAsync();

    Task<int> CreateReservationHoldAsync(ReservationHold reservationHold);
}