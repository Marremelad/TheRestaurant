using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories;

public class ReservationRepository(TheRestaurantDbContext context): IReservationRepository
{
    public async Task<List<Reservation>> GetReservationsAsync() =>
        await context.Reservations.ToListAsync();

    public async Task<List<Reservation>> GetReservationsByEmailAsync(string reservationEmail) =>
        await context.Reservations
            .Where(reservation => reservation.Email == reservationEmail)
            .ToListAsync();

    public async Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateOnly date) =>
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
    
    public async Task<Unit> DeleteReservationsAsync(List<Reservation> reservations)
    {
        context.Reservations.RemoveRange(reservations);
        await context.SaveChangesAsync();
        return Unit.Value;
    }
}