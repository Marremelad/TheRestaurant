using TheRestaurant.DTOs;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface IMenuItemService
{
    Task<ServiceResponse<List<MenuItemDto>>> GetMenuItemsAsync();

    Task<ServiceResponse<Unit>> CreateMenuItemAsync(MenuItemCreateDto menuItemDto);

    Task<ServiceResponse<Unit>> UpdateMenuItemAsync(int menuItemId, MenuItemUpdateDto menuItemUpdateDto);
}