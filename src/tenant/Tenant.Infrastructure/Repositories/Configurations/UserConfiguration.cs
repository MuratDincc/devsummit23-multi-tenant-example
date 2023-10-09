using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tenant.Infrastructure.Repositories.Configurations;

public class UserConfiguration : BaseEntityConfiguration<Entities.User>
{
    public override void Configure(EntityTypeBuilder<Entities.User> builder)
    {
        builder.Property(x => x.Name)
                    .HasMaxLength(100)
                    .IsRequired();

        builder.Property(x => x.Surname)
                    .HasMaxLength(100)
                    .IsRequired();
        
        builder.Property(x => x.Email)
                    .IsRequired();
        
        builder.Property(x => x.Password)
                    .IsRequired();
        
        builder.Property(x => x.Deleted)
                    .IsRequired();
        
        builder.Property(x => x.CreatedOnUtc)
                    .IsRequired()
                    .HasDefaultValue(DateTimeOffset.UtcNow);
        
        builder.Property(x => x.UpdatedOnUtc)
                    .IsRequired(false)
                    .HasDefaultValue(DateTimeOffset.UtcNow);

        base.Configure(builder);
    }
}
