using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories.IRepositories;

public interface IReservationRepository
{
    Task<List<Reservation>> GetReservationsAsync();

    Task<List<Reservation>> GetReservationsByEmailAsync(string reservationEmail);

    Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateOnly date);
    
    Task<Unit> CreateReservationAsync(Reservation reservation);

    Task<Unit> DeleteReservationsAsync(List<Reservation> reservation);

    Task<List<Reservation>> GetExpiredReservationsAsync(DateTime cutoffTime);
}