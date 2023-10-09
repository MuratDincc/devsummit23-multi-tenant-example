namespace Tenant.Domain.Entities;

public class Pool : BaseEntity
{
    public string Title { get; set; }
    public string Host { get; set; }
    public string Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Deleted { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }
}
