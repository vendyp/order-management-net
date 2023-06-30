using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderManagement.Core.Entities.Configurations;

public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    protected override void EntityConfiguration(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.ProductId);
        builder.Property(e => e.ProductId).ValueGeneratedNever();
        builder.Property(e => e.Name).HasMaxLength(256);
        builder.Property(e => e.Description).HasMaxLength(1024);
        builder.Property(e => e.Price).HasPrecision(18, 2);
    }
}