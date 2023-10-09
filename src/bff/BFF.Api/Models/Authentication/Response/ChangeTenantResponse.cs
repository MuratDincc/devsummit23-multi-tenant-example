namespace BFF.Api.Models.Authentication.Response;

public record ChangeTenantResponse
{
    public string Token { get; init; }
    public DateTime ExpiryDate { get; init; }
}
