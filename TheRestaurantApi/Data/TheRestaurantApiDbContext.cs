using Microsoft.EntityFrameworkCore;
using TheRestaurant.Models;

namespace TheRestaurant.Data;

public class TheRestaurantApiDbContext(DbContextOptions<TheRestaurantApiDbContext> options) 
    : DbContext(options)
{
    public DbSet<Table> Tables { get; set; }
    
    public DbSet<MenuItem> MenuItems { get; set; }
    
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
        
        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem
            {
                Id = 1,
                Name = "Grilled Salmon",
                Price = 24.99m,
                Description = "Fresh Atlantic salmon grilled to perfection, served with lemon herb butter, seasonal vegetables, and wild rice pilaf.",
                Image = "https://plus.unsplash.com/premium_photo-1723478417559-2349252a3dda?q=80&w=766&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            },
            new MenuItem
            {
                Id = 2,
                Name = "Margherita Pizza",
                Price = 16.50m,
                Description = "Classic wood-fired pizza with fresh mozzarella, San Marzano tomatoes, basil, and extra virgin olive oil on our signature sourdough crust.",
                Image = "https://images.unsplash.com/photo-1574071318508-1cdbab80d002?q=80&w=869&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            },
            new MenuItem
            {
                Id = 3,
                Name = "Caesar Salad",
                Price = 12.99m,
                Description = "Crisp romaine lettuce tossed with house-made Caesar dressing, parmesan cheese, croutons, and anchovies.",
                Image = "https://images.unsplash.com/photo-1546793665-c74683f339c1?q=80&w=387&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            },
            new MenuItem
            {
                Id = 4,
                Name = "Beef Tenderloin",
                Price = 32.00m,
                Description = "8oz premium beef tenderloin cooked to your preference, served with truffle mashed potatoes and roasted asparagus.",
                Image = "https://plus.unsplash.com/premium_photo-1723924821443-5bb2822e9a57?q=80&w=867&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            },
            new MenuItem
            {
                Id = 5,
                Name = "Chocolate Lava Cake",
                Price = 8.95m,
                Description = "Decadent warm chocolate cake with a molten center, served with vanilla ice cream and fresh berries.",
                Image = "https://images.unsplash.com/photo-1673551490812-eaee2e9bf0ef?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            }
        );
    }
}



