namespace Tenant.Api.Models.User.Request;

public record GetUserByEmailAndPasswordRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}
