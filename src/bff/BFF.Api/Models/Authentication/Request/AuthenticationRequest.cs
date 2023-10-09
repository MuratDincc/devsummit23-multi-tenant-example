namespace BFF.Api.Models.Authentication.Request;

public record AuthenticationRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}
