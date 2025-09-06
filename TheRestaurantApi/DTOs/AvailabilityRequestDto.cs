using System.ComponentModel.DataAnnotations;
using TheRestaurant.Utilities.IUtilities;

namespace TheRestaurant.DTOs;

public class AvailabilityRequestDto : IValidatable
{
    [Range(1, 10, ErrorMessage = "Party size has to be between 1 and 10.")]
    public int PartySize { get; set; }

    public DateOnly Date { get; set; }
}