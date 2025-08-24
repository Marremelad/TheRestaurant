using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories;

public class ReservationRepository(TheRestaurantDbContext context): IReservationRepository
{
    public Task<Unit> CreateReservation(Reservation reservation)
    {
        throw new NotImplementedException();
    }
}