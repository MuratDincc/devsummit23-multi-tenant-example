namespace Tenant.Domain.Entities;

public class PoolDatabase : BaseEntity
{
    public int PoolId { get; set; }
    public string Name { get; set; }
    public bool Deleted { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }

    public Pool Pool { get; set; }
}
