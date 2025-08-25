using System.ComponentModel.DataAnnotations;
using TheRestaurant.Utilities;

namespace TheRestaurant.Attributes;

public class ValidTableNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        
        var tracker = validationContext.GetService<DbSetTracker>();
        
        if (tracker == null)
            return ValidationResult.Success;

        if (value is not int tableNumber)
            return new ValidationResult("Table number must be a valid integer.");

        return !tracker.TableExists(tableNumber) 
            ? new ValidationResult("Table number does not exist.") 
            : ValidationResult.Success;
    }
}