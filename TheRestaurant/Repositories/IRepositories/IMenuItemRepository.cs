using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories.IRepositories;

public interface IMenuItemRepository
{
    Task<List<MenuItem>> GetMenuItemsAsync();

    Task<Unit> CreateMenuItemAsync(MenuItem menuItem);
}