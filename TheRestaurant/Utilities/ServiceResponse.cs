using System.Net;

namespace TheRestaurant.Utilities;

public record ServiceResponse<T>(HttpStatusCode StatusCode, T? Value, string Message)
{
    public static ServiceResponse<T> Success(HttpStatusCode statusCode, T value, string message) =>
        new(statusCode, value, message);

    public static ServiceResponse<T> Failure(HttpStatusCode statusCode, string message) =>
        new(statusCode, default, message);
}