namespace Tenant.Application.Queries.User.Dto;

public record GetUserDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }
    
    public List<int> TenantIds { get; init; }
}
