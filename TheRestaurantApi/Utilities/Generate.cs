using System.Net;
using Microsoft.AspNetCore.Mvc;
using TheRestaurant.Utilities.IUtilities;

namespace TheRestaurant.Utilities;

public static class Generate
{
    public static IActionResult ActionResult(IServiceResponse serviceResponse)
    {
          return serviceResponse.StatusCode switch
        {
            // 2xx Success
            HttpStatusCode.OK => new OkObjectResult(serviceResponse),
            HttpStatusCode.Created => new CreatedResult(string.Empty, serviceResponse),
            HttpStatusCode.Accepted => new AcceptedResult(string.Empty, serviceResponse),
            HttpStatusCode.NonAuthoritativeInformation => new ObjectResult(serviceResponse) { StatusCode = 203 },
            HttpStatusCode.NoContent => new NoContentResult(),
            HttpStatusCode.ResetContent => new ObjectResult(serviceResponse) { StatusCode = 205 },
            HttpStatusCode.PartialContent => new ObjectResult(serviceResponse) { StatusCode = 206 },
            
            // 3xx Redirection
            HttpStatusCode.MultipleChoices => new ObjectResult(serviceResponse) { StatusCode = 300 },
            HttpStatusCode.MovedPermanently => new ObjectResult(serviceResponse) { StatusCode = 301 },
            HttpStatusCode.Found => new ObjectResult(serviceResponse) { StatusCode = 302 },
            HttpStatusCode.SeeOther => new ObjectResult(serviceResponse) { StatusCode = 303 },
            HttpStatusCode.NotModified => new ObjectResult(serviceResponse) { StatusCode = 304 },
            HttpStatusCode.UseProxy => new ObjectResult(serviceResponse) { StatusCode = 305 },
            HttpStatusCode.TemporaryRedirect => new ObjectResult(serviceResponse) { StatusCode = 307 },
            HttpStatusCode.PermanentRedirect => new ObjectResult(serviceResponse) { StatusCode = 308 },
            
            // 4xx Client Errors
            HttpStatusCode.BadRequest => new BadRequestObjectResult(serviceResponse),
            HttpStatusCode.Unauthorized => new UnauthorizedResult(),
            HttpStatusCode.PaymentRequired => new ObjectResult(serviceResponse) { StatusCode = 402 },
            HttpStatusCode.Forbidden => new ForbidResult(),
            HttpStatusCode.NotFound => new NotFoundObjectResult(serviceResponse),
            HttpStatusCode.MethodNotAllowed => new ObjectResult(serviceResponse) { StatusCode = 405 },
            HttpStatusCode.NotAcceptable => new ObjectResult(serviceResponse) { StatusCode = 406 },
            HttpStatusCode.ProxyAuthenticationRequired => new ObjectResult(serviceResponse) { StatusCode = 407 },
            HttpStatusCode.RequestTimeout => new ObjectResult(serviceResponse) { StatusCode = 408 },
            HttpStatusCode.Conflict => new ConflictObjectResult(serviceResponse),
            HttpStatusCode.Gone => new ObjectResult(serviceResponse) { StatusCode = 410 },
            HttpStatusCode.LengthRequired => new ObjectResult(serviceResponse) { StatusCode = 411 },
            HttpStatusCode.PreconditionFailed => new ObjectResult(serviceResponse) { StatusCode = 412 },
            HttpStatusCode.RequestEntityTooLarge => new ObjectResult(serviceResponse) { StatusCode = 413 },
            HttpStatusCode.RequestUriTooLong => new ObjectResult(serviceResponse) { StatusCode = 414 },
            HttpStatusCode.UnsupportedMediaType => new ObjectResult(serviceResponse) { StatusCode = 415 },
            HttpStatusCode.RequestedRangeNotSatisfiable => new ObjectResult(serviceResponse) { StatusCode = 416 },
            HttpStatusCode.ExpectationFailed => new ObjectResult(serviceResponse) { StatusCode = 417 },
            HttpStatusCode.MisdirectedRequest => new ObjectResult(serviceResponse) { StatusCode = 421 },
            HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(serviceResponse),
            HttpStatusCode.Locked => new ObjectResult(serviceResponse) { StatusCode = 423 },
            HttpStatusCode.FailedDependency => new ObjectResult(serviceResponse) { StatusCode = 424 },
            HttpStatusCode.UpgradeRequired => new ObjectResult(serviceResponse) { StatusCode = 426 },
            HttpStatusCode.PreconditionRequired => new ObjectResult(serviceResponse) { StatusCode = 428 },
            HttpStatusCode.TooManyRequests => new ObjectResult(serviceResponse) { StatusCode = 429 },
            HttpStatusCode.RequestHeaderFieldsTooLarge => new ObjectResult(serviceResponse) { StatusCode = 431 },
            HttpStatusCode.UnavailableForLegalReasons => new ObjectResult(serviceResponse) { StatusCode = 451 },
            
            // 5xx Server Errors
            HttpStatusCode.InternalServerError => new ObjectResult(serviceResponse) { StatusCode = 500 },
            HttpStatusCode.NotImplemented => new ObjectResult(serviceResponse) { StatusCode = 501 },
            HttpStatusCode.BadGateway => new ObjectResult(serviceResponse) { StatusCode = 502 },
            HttpStatusCode.ServiceUnavailable => new ObjectResult(serviceResponse) { StatusCode = 503 },
            HttpStatusCode.GatewayTimeout => new ObjectResult(serviceResponse) { StatusCode = 504 },
            HttpStatusCode.HttpVersionNotSupported => new ObjectResult(serviceResponse) { StatusCode = 505 },
            HttpStatusCode.VariantAlsoNegotiates => new ObjectResult(serviceResponse) { StatusCode = 506 },
            HttpStatusCode.InsufficientStorage => new ObjectResult(serviceResponse) { StatusCode = 507 },
            HttpStatusCode.LoopDetected => new ObjectResult(serviceResponse) { StatusCode = 508 },
            HttpStatusCode.NotExtended => new ObjectResult(serviceResponse) { StatusCode = 510 },
            HttpStatusCode.NetworkAuthenticationRequired => new ObjectResult(serviceResponse) { StatusCode = 511 },
            
            // Default fallback for any unhandled status codes
            _ => new ObjectResult(serviceResponse.Message) { StatusCode = (int)serviceResponse.StatusCode }
        };
    }
}