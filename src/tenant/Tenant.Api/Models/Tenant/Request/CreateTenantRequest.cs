namespace Tenant.Api.Models.Tenant.Request;

public record CreateTenantRequest
{
    public int UserId  { get; init; }
    public string Title { get; init; }
    public string Slug { get; init; }
}
