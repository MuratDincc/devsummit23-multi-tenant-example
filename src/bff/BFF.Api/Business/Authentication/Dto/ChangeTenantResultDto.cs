namespace BFF.Api.Business.Authentication.Dto;

public record ChangeTenantResultDto
{
    public string Token { get; init; }
    public DateTime ExpiryDate { get; init; }
}