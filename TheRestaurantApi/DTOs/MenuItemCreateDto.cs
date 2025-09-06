using System.ComponentModel.DataAnnotations;
using TheRestaurant.Utilities.IUtilities;

namespace TheRestaurant.DTOs;

public class MenuItemCreateDto : IValidatable
{
    [Required(ErrorMessage = "Menu item name is required.")]
    [StringLength(50, ErrorMessage = "Menu item name can not be longer than 50 characters.")]
    public required string Name { get; init; }

    public decimal Price { get; init; }
    
    [Required(ErrorMessage = "Description is required.")]
    [StringLength(300, ErrorMessage = "Description can not be longer than 300 characters.")]
    public required string Description { get; init; }

    [Required(ErrorMessage = "Image url is required.")]
    [StringLength(500, ErrorMessage = "Image url can not be longer than 500 characters.")]
    public required string Image { get; init; }
    
    [Required(ErrorMessage = "The popular tag has to be set.")]
    public required bool IsPopular { get; init; }
}
    