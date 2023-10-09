namespace BFF.Api.Models.Tenant.Request;

public record UpdateTenantRequest
{
    public string Title { get; init; }
    public string Slug { get; init; }
}
