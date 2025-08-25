using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories;

public class ReservationRepository(TheRestaurantDbContext context): IReservationRepository
{
    public async Task<IEnumerable<Reservation>> GetReservationsAsync() =>
        await context.Reservations.ToListAsync();

    public async Task<Reservation?> GetReservationAsync(int reservationId) =>
        await context.Reservations.FirstOrDefaultAsync(reservation => reservation.Id == reservationId);
    
    public async Task<Unit> CreateReservationAsync(Reservation reservation)
    {
        context.Reservations.Add(reservation);
        await context.SaveChangesAsync();
        return Unit.Value;
    }

    public async Task<Unit> DeleteReservationAsync(Reservation reservation)
    {
        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync();
        return Unit.Value;
    }
}