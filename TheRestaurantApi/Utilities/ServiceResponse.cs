using System.Net;
using TheRestaurant.Utilities.IUtilities;

namespace TheRestaurant.Utilities;

/// <summary>
/// Generic wrapper for service layer responses that standardizes HTTP status codes, data, and messages across the application.
/// </summary>
public record ServiceResponse<T>(
    bool IsSuccess,
    HttpStatusCode StatusCode,
    T? Value,
    string Message
    ) : IServiceResponse
{
    /// <summary>
    /// Creates a successful response with data, typically used for operations that return information.
    /// </summary>
    public static ServiceResponse<T> Success(HttpStatusCode statusCode, T value, string message) =>
        new(true, statusCode, value, message);

    /// <summary>
    /// Creates a failure response without data, used for error conditions and validation failures.
    /// </summary>
    public static ServiceResponse<T> Failure(HttpStatusCode statusCode, string message) =>
        new(false, statusCode, default, message);
}