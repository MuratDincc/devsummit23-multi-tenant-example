using Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commerce.Infrastructure.Repositories.Configurations;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
    }
}
