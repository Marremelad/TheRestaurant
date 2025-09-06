namespace TheRestaurant.DTOs;

public record MenuItemDto(
    int Id,
    string Name,
    decimal Price,
    string Description,
    string Image,
    bool IsPopular
    );