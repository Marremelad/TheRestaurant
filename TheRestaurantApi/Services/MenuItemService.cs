using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

/// <summary>
/// Manages menu item operations including retrieval, creation, and updates for restaurant menu management.
/// </summary>
public class MenuItemService(
    IMenuItemRepository repository,
    ILogger<MenuItemService> logger
    ) : IMenuItemService
{
    /// <summary>
    /// Retrieves all menu items from the database and converts them to DTOs for API response.
    /// </summary>
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

    /// <summary>
    /// Creates a new menu item from the provided DTO data and saves it to the database.
    /// </summary>
    public async Task<ServiceResponse<Unit>> CreateMenuItemAsync(MenuItemCreateDto menuItemDto)
    {
        return await menuItemDto.ValidateAndExecuteAsync(async () =>
        {
            try
            {
                // Convert DTO to entity model for database storage.
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
                    $"{message}: {ex.Message}"
                );
            }
        });
    }

    /// <summary>
    /// Updates an existing menu item with partial data using PATCH semantics (only non-null fields are updated).
    /// </summary>
    public async Task<ServiceResponse<Unit>> UpdateMenuItemAsync(int menuItemId, MenuItemUpdateDto menuItemUpdateDto)
    {
        try
        {
            // Retrieve the existing menu item to verify it exists.
            var menuItem = await repository.GetMenuItemByIdAsync(menuItemId);
        
            if (menuItem == null)
                return ServiceResponse<Unit>.Failure(
                    HttpStatusCode.NotFound,
                    $"Menu item with ID {menuItem} not found."
                );

            // Apply only the non-null fields from the update DTO to the existing entity.
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