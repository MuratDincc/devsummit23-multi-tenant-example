namespace BFF.Api.Services.Tenant.Models.Request;

public record PatchUserInformationServiceRequest
{
    public string Name { get; init; }
    public string Surname { get; init; }
}
