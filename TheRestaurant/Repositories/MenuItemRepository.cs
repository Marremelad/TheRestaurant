using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories;

public class MenuItemRepository(TheRestaurantDbContext context) : IMenuItemRepository
{
    public async Task<List<MenuItem>> GetMenuItemsAsync() =>
        await context.MenuItems.ToListAsync();
    

    public async Task<Unit> CreateMenuItemAsync(MenuItem menuItem)
    {
        context.MenuItems.Add(menuItem);
        await context.SaveChangesAsync();
        return Unit.Value;
    }
}