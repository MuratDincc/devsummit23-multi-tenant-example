namespace BFF.Api.Models.Tenant.Response;

public record GetTenantsResponse
{
    public List<GetTenantResponse> Tenants { get; init; }
}
