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

    public async Task<Reservation?> GetReservationAsync(string reservationEmail) =>
        await context.Reservations.FirstOrDefaultAsync(reservation => reservation.Email == reservationEmail);

    public async Task<IEnumerable<Reservation>> GetReservationsByDate(DateOnly date) =>
        await context.Reservations
            .Include(reservation => reservation.Table)
            .Where(reservation => reservation.Date == date)
            .ToListAsync();

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