using System.ComponentModel.DataAnnotations;

namespace TheRestaurant.DTOs;

public record TableDto(
    [Range(1, int.MaxValue, ErrorMessage = "Table number can not be 0.")] int Number,
    int Capacity
    );