using System.ComponentModel.DataAnnotations;
using TheRestaurant.Utilities.IUtilities;

namespace TheRestaurant.DTOs;

public record AvailabilityRequestDto(
    [Range(1, 10)] int PartySize,
    DateOnly Date
    ): IValidatable;