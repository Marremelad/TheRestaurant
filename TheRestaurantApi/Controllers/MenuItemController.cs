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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMenuItemById([FromRoute(Name = "id")] int id) =>
        Generate.ActionResult(await service.GetMenuItemByIdAsync(id));

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateMenuItem(MenuItemCreateDto menuItemCreateDto) =>
        Generate.ActionResult(await service.CreateMenuItemAsync(menuItemCreateDto));
    
    [HttpPatch("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateMenuItem([FromRoute(Name = "id")] int menuItemId, MenuItemUpdateDto menuItemUpdateDto) =>
        Generate.ActionResult(await service.UpdateMenuItemAsync(menuItemId, menuItemUpdateDto));

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteMenuItem([FromRoute(Name = "id")] int id) =>
        Generate.ActionResult(await service.DeleteMenuItemAsync(id));
}