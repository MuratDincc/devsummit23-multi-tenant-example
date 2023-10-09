using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tenant.Infrastructure.Repositories.Configurations;

public class PoolDatabaseConfiguration : BaseEntityConfiguration<Entities.PoolDatabase>
{
    public override void Configure(EntityTypeBuilder<Entities.PoolDatabase> builder)
    {
        builder.Property(x => x.PoolId)
                    .IsRequired();

        builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);

        builder.Property(x => x.Deleted)
                    .IsRequired();
        
        builder.Property(x => x.CreatedOnUtc)
                    .IsRequired()
                    .HasDefaultValue(DateTimeOffset.UtcNow);
        
        builder.Property(x => x.UpdatedOnUtc)
                    .IsRequired(false)
                    .HasDefaultValue(DateTimeOffset.UtcNow);
        
        builder.HasOne(x => x.Pool)
                    .WithMany()
                    .HasForeignKey(x => x.PoolId);

        base.Configure(builder);
    }
}
