using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

public class MenuItemService(
    IMenuItemRepository repository,
    ILogger<MenuItemService> logger
    ) : IMenuItemService
{
    public async Task<ServiceResponse<List<MenuItemDto>>> GetMenuItemsAsync()
    {
        try
        {
            var menuItems = await repository.GetMenuItemsAsync();

            return ServiceResponse<List<MenuItemDto>>.Success(
                HttpStatusCode.OK,
                MenuItemMapper.ToDtos(menuItems),
                "Menu items fetched successfully"
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch menu items.";
            logger.LogError(ex, message);
            return ServiceResponse<List<MenuItemDto>>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }

    public async Task<ServiceResponse<Unit>> CreateMenuItemAsync(MenuItemCreateDto menuItemDto)
    {
        try
        {
            var menuItem = MenuItemMapper.ToEntity(menuItemDto);

            return ServiceResponse<Unit>.Success(
                HttpStatusCode.OK,
                await repository.CreateMenuItemAsync(menuItem),
                "Menu item created successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to create menu item.";
            logger.LogError(ex, message);
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<Unit>> UpdateMenuItemAsync(int menuItemId, MenuItemUpdateDto menuItemUpdateDto)
    {
        try
        {
            var menuItem = await repository.GetMenuItemByIdAsync(menuItemId);
        
            if (menuItem == null)
                return ServiceResponse<Unit>.Failure(
                    HttpStatusCode.NotFound,
                    $"Menu item with ID {menuItem} not found."
                );

            MenuItemMapper.ApplyUpdates(menuItem, menuItemUpdateDto);

            return ServiceResponse<Unit>.Success(
                HttpStatusCode.OK,
                await repository.UpdateMenuItemAsync(menuItem),
                "Menu item updated successfully."
            );
            
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while updating menu item.";
            logger.LogError(ex, message);
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }
}