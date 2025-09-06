using System.ComponentModel.DataAnnotations;
using TheRestaurant.Utilities.IUtilities;

namespace TheRestaurant.DTOs;

public class TableDto : IValidatable
{
    [Range(1, int.MaxValue, ErrorMessage = "Table number can not be 0.")]
    public int Number { get; init; }

    [Range(1, 10, ErrorMessage = "Table capacity has to be at least 1 and can not exceed 10")]
    public int Capacity { get; init; }
}