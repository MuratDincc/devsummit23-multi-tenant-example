namespace BFF.Api.Services.Tenant.Models.Request;

public record CreateTenantServiceRequest
{
    public int UserId  { get; init; }
    public string Title { get; init; }
    public string Slug { get; init; }
}