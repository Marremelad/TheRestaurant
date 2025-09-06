using System.ComponentModel.DataAnnotations;
using System.Net;
using TheRestaurant.Utilities.IUtilities;

namespace TheRestaurant.Utilities;

public static class ValidationExtensions
{
    public static async Task<ServiceResponse<T>> ValidateAndExecuteAsync<T>(
        this IValidatable dto,
        Func<Task<ServiceResponse<T>>> operation
    )
    {
        var validationResult = ValidateDto(dto);
        return validationResult.IsValid
            ? await operation()
            : ServiceResponse<T>.Failure(
                HttpStatusCode.BadRequest,
                validationResult.ErrorMessage
            );
    }
    
    private static (bool IsValid, string ErrorMessage) ValidateDto(object dto)
    {
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        
        return Validator.TryValidateObject(dto, context, results, true) 
            ? (true, string.Empty) 
            : (false, string.Join("; ", results.Select(r => r.ErrorMessage)));
    }
}