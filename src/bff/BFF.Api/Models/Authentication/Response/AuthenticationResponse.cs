namespace BFF.Api.Models.Authentication.Response;

public record AuthenticationResponse
{
    public string Token { get; init; }
    public DateTime ExpiryDate { get; init; }
}
