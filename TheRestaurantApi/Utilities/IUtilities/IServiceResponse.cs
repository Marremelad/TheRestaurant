using System.Net;

namespace TheRestaurant.Utilities.IUtilities;

public interface IServiceResponse
{
    HttpStatusCode StatusCode { get; }
    string Message { get; }
}