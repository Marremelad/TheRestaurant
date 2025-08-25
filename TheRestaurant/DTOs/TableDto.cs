using System.ComponentModel.DataAnnotations;

namespace TheRestaurant.DTOs;

public record TableDto(
    [Range(1, int.MaxValue, ErrorMessage = "Table number can not be 0.")] int Number,
    [Range(1, 10, ErrorMessage = "Table capacity has to be at least 1 and can not exceed 10")] int Capacity
    );