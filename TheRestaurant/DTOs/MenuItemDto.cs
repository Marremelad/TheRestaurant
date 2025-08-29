using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheRestaurant.DTOs;

public record MenuItemDto(
    [StringLength(50)] string Name,
    decimal Price,
    [StringLength(300)] string Description,
    [StringLength(500)] string Image
    );