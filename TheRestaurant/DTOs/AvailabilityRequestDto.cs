using System.ComponentModel.DataAnnotations;

namespace TheRestaurant.DTOs;

public record AvailabilityRequestDto(
    [Range(1, 10)] int PartySize,
    DateOnly Date
    );