namespace BFF.Api.Services.Tenant.Models.Request;

public record AuthenticationServiceRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}