using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories;

public class MenuItemRepository(TheRestaurantApiDbContext context) : IMenuItemRepository
{
    public async Task<List<MenuItem>> GetMenuItemsAsync() =>
        await context.MenuItems.ToListAsync();

    public async Task<MenuItem?> GetMenuItemByIdAsync(int menuItemId) =>
        await context.MenuItems.FirstOrDefaultAsync(menuItem => menuItem.Id == menuItemId);
    
    public async Task<Unit> CreateMenuItemAsync(MenuItem menuItem)
    {
        context.MenuItems.Add(menuItem);
        await context.SaveChangesAsync();
        return Unit.Value;
    }

    public async Task<Unit> UpdateMenuItemAsync(MenuItem menuItem)
    {
        context.MenuItems.Update(menuItem);
        await context.SaveChangesAsync();
        return Unit.Value;
    }

    public async Task<Unit> DeleteMenuItemAsync(MenuItem menuItem)
    {
        context.MenuItems.Remove(menuItem);
        await context.SaveChangesAsync();
        return Unit.Value;
    }
}