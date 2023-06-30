using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderManagement.Core.Entities.Configurations;

public class OrderItemConfiguration : BaseEntityConfiguration<OrderItem>
{
    protected override void EntityConfiguration(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(e => e.OrderItemId);
        builder.Property(e => e.OrderItemId).ValueGeneratedNever();
        builder.Property(e => e.TotalPrice).HasPrecision(18, 2);
    }
}