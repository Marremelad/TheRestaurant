using System.ComponentModel.DataAnnotations;

namespace TheRestaurant.DTOs;

public record LoginDto(
    [Required] string UserName,
    [Required] string Password
    );