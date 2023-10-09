namespace BFF.Api.Models.Tenant.Request;

public record CreateTenantRequest
{
    public string Title { get; init; }
    public string Slug { get; init; }
}
