namespace Tenant.Application.Queries.Tenant.Dto;

public record GetTenantsDto
{
    public List<GetTenantDto> Tenants { get; init; }
}
