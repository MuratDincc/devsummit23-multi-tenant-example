namespace BFF.Api.Services.Tenant.Models.Request;

public record CreateUserServiceRequest
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}
