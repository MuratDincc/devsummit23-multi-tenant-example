namespace Tenant.Domain.Entities;

public class Tenant : BaseEntity
{
    public Guid AliasId { get; set; }
    public int PoolDatabaseId { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public bool Deleted { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }
    
    public PoolDatabase PoolDatabase { get; set; }
}
