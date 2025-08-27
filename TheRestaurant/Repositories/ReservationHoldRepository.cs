using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;

namespace TheRestaurant.Repositories;

public class ReservationHoldRepository(TheRestaurantDbContext context) : IReservationHoldRepository
{
    public async Task<List<ReservationHold>> GetReservationHoldsAsync() =>
        await context.ReservationHolds.ToListAsync();
}