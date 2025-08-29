using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheRestaurant.DTOs;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Controllers;

[Route("api/menu-items")]
[ApiController]
public class MenuItemController(IMenuItemService service)
{
    [HttpGet]
    public async Task<IActionResult> GetMenuItems() =>
        Generate.ActionResult(await service.GetMenuItemsAsync());

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateMenuItem(MenuItemDto menuItemDto) =>
        Generate.ActionResult(await service.CreateMenuItemAsync(menuItemDto));
}