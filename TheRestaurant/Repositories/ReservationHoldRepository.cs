using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories;

public class ReservationHoldRepository(TheRestaurantDbContext context) : IReservationHoldRepository
{
    public async Task<ReservationHold?> GetReservationHoldByIdAsync(int reservationHoldId) =>
        await context.ReservationHolds.FirstOrDefaultAsync(reservationHold => reservationHold.Id == reservationHoldId);

    public async Task<List<ReservationHold>> GetReservationHoldsAsync() =>
        await context.ReservationHolds.ToListAsync();

    public async Task<int> CreateReservationHoldAsync(ReservationHold reservationHold)
    {
        context.ReservationHolds.Add(reservationHold);
        await context.SaveChangesAsync();
        return reservationHold.Id;
    }

    public async Task<Unit> DeleteReservationHoldAsync(ReservationHold reservationHold)
    {
        context.ReservationHolds.Remove(reservationHold);
        await context.SaveChangesAsync();
        return Unit.Value;
    }
}