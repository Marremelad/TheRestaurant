using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories.IRepositories;

public interface IReservationRepository
{
    Task<Unit> CreateReservation(Reservation reservation);
}