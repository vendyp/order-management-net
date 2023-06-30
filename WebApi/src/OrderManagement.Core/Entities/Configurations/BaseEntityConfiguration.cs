using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Core.Entities.Abstractions;

namespace OrderManagement.Core.Entities.Configurations;

public abstract class BaseEntityConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity>
    where TBaseEntity : BaseEntity
{
    public void Configure(EntityTypeBuilder<TBaseEntity> builder)
    {
        builder.Property(e => e.CreatedBy).HasMaxLength(maxLength: 256);
        builder.Property(e => e.CreatedByName).HasMaxLength(maxLength: 256);

        builder.Property(e => e.LastUpdatedBy).HasMaxLength(maxLength: 256);
        builder.Property(e => e.LastUpdatedByName).HasMaxLength(maxLength: 256);

        builder.HasQueryFilter(e => e.IsDeleted == false);

        EntityConfiguration(builder);
    }

    protected abstract void EntityConfiguration(EntityTypeBuilder<TBaseEntity> builder);
}