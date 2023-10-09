namespace Tenant.Api.Models.User.Response;

public record GetUserResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public DateTimeOffset CreatedOnUtc { get; init; }
    public DateTimeOffset? UpdatedOnUtc { get; init; }
    
    public List<int> TenantIds { get; init; }
}
