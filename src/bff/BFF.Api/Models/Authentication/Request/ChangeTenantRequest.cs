namespace BFF.Api.Models.Authentication.Request;

public record ChangeTenantRequest
{
    public int TenantId { get; init; }
}
