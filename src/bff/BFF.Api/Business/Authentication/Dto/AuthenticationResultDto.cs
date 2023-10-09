namespace BFF.Api.Business.Authentication.Dto;

public record AuthenticationResultDto
{
    public string Token { get; init; }
    public DateTime ExpiryDate { get; init; }
}