using OrderManagement.Core.Entities.Abstractions;

namespace OrderManagement.Core.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderItemId { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}