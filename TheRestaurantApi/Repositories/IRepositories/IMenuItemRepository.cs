using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories.IRepositories;

public interface IMenuItemRepository
{
    Task<List<MenuItem>> GetMenuItemsAsync();

    Task<MenuItem?> GetMenuItemByIdAsync(int menuItemId);

    Task<Unit> CreateMenuItemAsync(MenuItem menuItem);

    Task<Unit> UpdateMenuItemAsync(MenuItem menuItem);

    Task<Unit> DeleteMenuItemAsync(MenuItem menuItem);
}