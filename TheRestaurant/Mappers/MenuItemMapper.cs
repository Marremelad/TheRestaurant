using TheRestaurant.DTOs;
using TheRestaurant.Models;

namespace TheRestaurant.Mappers;

public static class MenuItemMapper
{
    public static MenuItemDto ToDto(MenuItem menuItem) =>
        new(
            menuItem.Name,
            menuItem.Price,
            menuItem.Description,
            menuItem.Image
        );

    public static List<MenuItemDto> ToDtos(IEnumerable<MenuItem> menuItems) =>
        menuItems.Select(ToDto).ToList();

    public static MenuItem ToEntity(MenuItemDto menuItemDto) =>
        new()
        {
            Name = menuItemDto.Name,
            Price = menuItemDto.Price,
            Description = menuItemDto.Description,
            Image = menuItemDto.Image
        };
    
}