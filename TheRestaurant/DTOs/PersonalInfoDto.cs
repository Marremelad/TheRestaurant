using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Extensions.Options;
using TheRestaurant.Utilities.IUtilities;

namespace TheRestaurant.DTOs;

public class PersonalInfoDto : IValidatable
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name can not be longer than 50 characters.")]
    public required string FirstName { get; init; }
    
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name can not be longer than 50 characters.")]
    public required string LastName { get; init; }
    
    [Required(ErrorMessage = "Email address is required.")]
    [StringLength(258, ErrorMessage = "Email address can not be longer than 258 characters.")]
    [EmailAddress(ErrorMessage = "Not a valid email address.")]
    public required string Email { get; init; }
}