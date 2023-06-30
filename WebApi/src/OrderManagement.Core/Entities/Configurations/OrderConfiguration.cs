using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderManagement.Core.Entities.Configurations;

public class OrderConfiguration : BaseEntityConfiguration<Order>
{
    protected override void EntityConfiguration(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.OrderId);
        builder.Property(e => e.OrderId).ValueGeneratedNever();
        builder.Property(e => e.OrderNumber).HasMaxLength(1024);
        builder.Property(e => e.TotalPrice).HasPrecision(18, 2);
    }
}