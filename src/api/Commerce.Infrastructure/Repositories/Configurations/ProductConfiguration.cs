using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commerce.Infrastructure.Repositories.Configurations;

public class ProductConfiguration : BaseEntityConfiguration<Entities.Product>
{
    public override void Configure(EntityTypeBuilder<Entities.Product> builder)
    {
        builder.Property(x => x.Title)
                    .IsRequired();
        
        builder.Property(x => x.Price)
            .IsRequired();
        
        builder.Property(x => x.Image)
            .IsRequired(false);

        builder.Property(x => x.Deleted)
                    .IsRequired();

        base.Configure(builder);
    }
}
