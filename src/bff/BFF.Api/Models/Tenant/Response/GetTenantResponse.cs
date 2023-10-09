namespace BFF.Api.Models.Tenant.Response;

public record GetTenantResponse
{
    public int Id { get; init; }
    public Guid AliasId { get; init; }
    public string Title { get; init; }
    public string Slug { get; init; }
    public string ConnectionString { get; init; }
}
