using TheRestaurant.DTOs;
using TheRestaurant.Models;

namespace TheRestaurant.Mappers;

public static class MenuItemMapper
{
    public static MenuItemDto ToDto(MenuItem menuItem) =>
        new(
            menuItem.Id,
            menuItem.Name,
            menuItem.Price,
            menuItem.Description,
            menuItem.Image,
            menuItem.IsPopular
        );

    public static List<MenuItemDto> ToDtos(IEnumerable<MenuItem> menuItems) =>
        menuItems.Select(ToDto).ToList();

    public static MenuItem ToEntity(MenuItemCreateDto menuItemDto) =>
        new()
        {
            Name = menuItemDto.Name,
            Price = menuItemDto.Price,
            Description = menuItemDto.Description,
            Image = menuItemDto.Image,
            IsPopular = menuItemDto.IsPopular
        };
    
    public static void ApplyUpdates(MenuItem menuItem, MenuItemUpdateDto updateDto)
    {
        if (updateDto.Name != null) 
            menuItem.Name = updateDto.Name;
        
        if (updateDto.Price != null) 
            menuItem.Price = updateDto.Price.Value;
        
        if (updateDto.Description != null) 
            menuItem.Description = updateDto.Description;
        
        if (updateDto.Image != null) 
            menuItem.Image = updateDto.Image;

        if (updateDto.IsPopular != null)
            menuItem.IsPopular = updateDto.IsPopular;
    }
}