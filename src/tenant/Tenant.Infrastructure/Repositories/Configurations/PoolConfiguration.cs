using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tenant.Infrastructure.Repositories.Configurations;

public class PoolConfiguration : BaseEntityConfiguration<Entities.Pool>
{
    public override void Configure(EntityTypeBuilder<Entities.Pool> builder)
    {
        builder.Property(x => x.Title)
                    .IsRequired();

        builder.Property(x => x.Host)
                    .IsRequired();
        
        builder.Property(x => x.Port)
                    .IsRequired()
                    .HasMaxLength(5);

        builder.Property(x => x.Username)
                    .IsRequired();
        
        builder.Property(x => x.Password)
                    .IsRequired();

        base.Configure(builder);
    }
}
