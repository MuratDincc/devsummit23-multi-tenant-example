namespace BFF.Api.Services.Tenant.Models.Response;

public record CreateTenantServiceResponse
{
    public Guid Id { get; init; }
    public string Slug { get; init; }
    public string DatabaseName { get; init; }
    public string ConnectionString { get; init; }
}