using Microsoft.EntityFrameworkCore;
using TheRestaurant.Models;

namespace TheRestaurant.Data;

public class TheRestaurantDbContext(DbContextOptions<TheRestaurantDbContext> options) 
    : DbContext(options)
{
    public DbSet<Table> Tables { get; set; }
}



