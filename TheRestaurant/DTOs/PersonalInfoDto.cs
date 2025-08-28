using System.ComponentModel.DataAnnotations;

namespace TheRestaurant.DTOs;

public record PersonalInfoDto(
    [StringLength(50)] string FirstName,
    [StringLength(50)] string LastName,
    [EmailAddress] string Email 
    );