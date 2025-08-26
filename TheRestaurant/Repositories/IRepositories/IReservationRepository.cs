using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories.IRepositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetReservationsAsync();

    Task<Reservation?> GetReservationAsync(string reservationEmail);

    Task<IEnumerable<Reservation>> GetReservationsByDate(DateOnly date);
    
    Task<Unit> CreateReservationAsync(Reservation reservation);

    Task<Unit> DeleteReservationAsync(Reservation reservation);
}