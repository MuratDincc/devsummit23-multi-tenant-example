namespace Tenant.Api.Models.Tenant.Response;

public record CreateTenantResponse
{
    public Guid Id { get; init; }
    public string Slug { get; init; }
    public string DatabaseName { get; init; }
    public string ConnectionString { get; init; }
}
