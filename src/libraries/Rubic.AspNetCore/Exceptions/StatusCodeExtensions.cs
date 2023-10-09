using System.Net;

namespace Rubic.AspNetCore.Exceptions;

public static class StatusCodeExtensions
{
    public static HttpStatusCode ToHttpStatusCode(this StatusCode code)
    {
        switch (code)
        {
            case StatusCode.Success:
                return HttpStatusCode.OK;
            case StatusCode.BadRequest:
                return HttpStatusCode.BadRequest;
            case StatusCode.NotFound:
                return HttpStatusCode.NotFound;
            case StatusCode.Unauthenticated:
                return HttpStatusCode.Unauthorized;
            case StatusCode.Unauthorized:
                return HttpStatusCode.Forbidden;
            case StatusCode.RequestTimeout:
                return HttpStatusCode.RequestTimeout;
            case StatusCode.Conflict:
                return HttpStatusCode.Conflict;
            case StatusCode.PreconditionFailed:
                return HttpStatusCode.PreconditionFailed;
            case StatusCode.UnsupportedMediaType:
                return HttpStatusCode.UnsupportedMediaType;
            case StatusCode.UnprocessableEntity:
                return HttpStatusCode.UnprocessableEntity;
            default:
                throw new ArgumentException($"There is no equality of the StatusCode for the HttpStatusCode {code}");
        }
    }

    public static StatusCode ToStatusCode(this HttpStatusCode code)
    {
        switch (code)
        {
            case HttpStatusCode.OK:
                return StatusCode.Success;
            case HttpStatusCode.BadRequest:
                return StatusCode.BadRequest;
            case HttpStatusCode.NotFound:
                return StatusCode.NotFound;
            case HttpStatusCode.Unauthorized:
                return StatusCode.Unauthenticated;
            case HttpStatusCode.Forbidden:
                return StatusCode.Unauthorized;
            case HttpStatusCode.RequestTimeout:
                return StatusCode.RequestTimeout;
            case HttpStatusCode.Conflict:
                return StatusCode.Conflict;
            case HttpStatusCode.PreconditionFailed:
                return StatusCode.PreconditionFailed;
            case HttpStatusCode.UnsupportedMediaType:
                return StatusCode.UnsupportedMediaType;
            case HttpStatusCode.UnprocessableEntity:
                return StatusCode.UnprocessableEntity;
            default:
                return StatusCode.None;
        }
    }
}
