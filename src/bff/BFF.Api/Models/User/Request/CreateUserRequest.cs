namespace BFF.Api.Models.User.Request;

public record CreateUserRequest
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}
