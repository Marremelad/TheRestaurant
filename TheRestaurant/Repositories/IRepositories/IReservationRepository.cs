using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories.IRepositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetReservationsAsync();

    Task<Reservation?> GetReservationAsync(int reservationId);
    
    Task<Unit> CreateReservationAsync(Reservation reservation);

    Task<Unit> DeleteReservationAsync(Reservation reservation);
}