namespace Tenant.Domain.Entities;

public class TenantUser : BaseEntity
{
    public int TenantId { get; set; }
    public int UserId { get; set; }
    
    public Tenant Tenant { get; set; }
    public User User { get; set; }
}
