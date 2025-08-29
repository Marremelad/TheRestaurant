using Microsoft.EntityFrameworkCore;
using TheRestaurant.Models;

namespace TheRestaurant.Data;

public class TheRestaurantDbContext(DbContextOptions<TheRestaurantDbContext> options) 
    : DbContext(options)
{
    public DbSet<Table> Tables { get; set; }
    
    public DbSet<Reservation> Reservations { get; set; }
    
    public DbSet<ReservationHold> ReservationHolds { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            UserName = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            CreatedAt = DateTime.UtcNow
        });
    }
}



