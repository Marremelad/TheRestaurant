using System.ComponentModel.DataAnnotations;

namespace TheRestaurant.DTOs;

public record MenuItemUpdateDto(
    string? Name = null,
    decimal? Price = null,
    string? Description = null,
    string? Image = null
    );