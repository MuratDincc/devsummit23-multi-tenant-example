namespace Rubic.AspNetCore.Exceptions;

public enum StatusCode
{
    None = 0,
    Success = 1,
    BadRequest = 2,
    NotFound = 3,
    Unauthenticated = 4,
    Unauthorized = 5,
    RequestTimeout = 6,
    Conflict = 7,
    PreconditionFailed = 8,
    UnsupportedMediaType = 9,
    UnprocessableEntity = 10
}
