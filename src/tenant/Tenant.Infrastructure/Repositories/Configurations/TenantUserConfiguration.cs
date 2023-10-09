using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tenant.Infrastructure.Repositories.Configurations;

public class TenantUserConfiguration : BaseEntityConfiguration<Entities.TenantUser>
{
    public override void Configure(EntityTypeBuilder<Entities.TenantUser> builder)
    {
        builder.Property(x => x.TenantId)
                    .IsRequired();

        builder.Property(x => x.UserId)
                    .IsRequired();

        builder.HasOne(x => x.Tenant)
                    .WithMany()
                    .HasForeignKey(x => x.TenantId);
        
        builder.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId);

        base.Configure(builder);
    }
}
