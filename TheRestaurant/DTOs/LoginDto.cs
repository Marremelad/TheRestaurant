using System.ComponentModel.DataAnnotations;
using TheRestaurant.Utilities.IUtilities;

namespace TheRestaurant.DTOs;

public class LoginDto : IValidatable
{
    [Required(ErrorMessage = "Username is required.")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; set; }
}