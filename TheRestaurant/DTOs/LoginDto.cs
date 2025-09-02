using System.ComponentModel.DataAnnotations;
using TheRestaurant.Utilities.IUtilities;

namespace TheRestaurant.DTOs;

public record LoginDto(
    [Required] string UserName,
    [Required] string Password
    ): IValidatable;