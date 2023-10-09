namespace BFF.Api.Models.User.Request;

public record PatchUserInformationRequest
{
    public string Name { get; init; }
    public string Surname { get; init; }
}
