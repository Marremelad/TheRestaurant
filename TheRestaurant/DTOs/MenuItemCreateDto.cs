using System.ComponentModel.DataAnnotations;

namespace TheRestaurant.DTOs;

public record MenuItemCreateDto(
    [StringLength(50)] string Name,
    decimal Price,
    [StringLength(300)] string Description,
    [StringLength(500)] string Image
    );