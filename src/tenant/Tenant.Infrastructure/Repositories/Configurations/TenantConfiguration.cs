using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tenant.Infrastructure.Repositories.Configurations;

public class TenantConfiguration : BaseEntityConfiguration<Entities.Tenant>
{
    public override void Configure(EntityTypeBuilder<Entities.Tenant> builder)
    {
        builder.Property(x => x.AliasId)
                    .IsRequired();

        builder.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(250);
        
        builder.Property(x => x.Deleted)
                    .IsRequired();
        
        builder.Property(x => x.CreatedOnUtc)
                    .IsRequired()
                    .HasDefaultValue(DateTimeOffset.UtcNow);
        
        builder.Property(x => x.UpdatedOnUtc)
                    .IsRequired(false)
                    .HasDefaultValue(DateTimeOffset.UtcNow);
        
        builder.HasOne(x => x.PoolDatabase)
                    .WithMany()
                    .HasForeignKey(x => x.PoolDatabaseId);

        base.Configure(builder);
    }
}
