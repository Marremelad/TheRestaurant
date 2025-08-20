using Microsoft.EntityFrameworkCore;

namespace TheRestaurant.Data;

public class TheRestaurantDbContext(DbContextOptions<TheRestaurantDbContext> options) 
    : DbContext(options)
{
    
}