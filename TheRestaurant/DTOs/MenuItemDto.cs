using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheRestaurant.DTOs;

public record MenuItemDto(
    int Id,
    string Name,
    decimal Price,
    string Description,
    string Image
    );