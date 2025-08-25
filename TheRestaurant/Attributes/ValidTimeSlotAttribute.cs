using System.ComponentModel.DataAnnotations;
using TheRestaurant.Enums;

namespace TheRestaurant.Attributes;

public class ValidTimeSlotAttribute : ValidationAttribute
{
    private static readonly int[] ValidTimeSlots = Enum.GetValues<TimeSlot>().Cast<int>().ToArray();
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not int timeSlot)
            return new ValidationResult("Time slot must be a valid integer.");

        return ValidTimeSlots.Contains(timeSlot)
            ? ValidationResult.Success
            : new ValidationResult($"Time slot must be one of: {string.Join(", ", ValidTimeSlots)}");
    }
}