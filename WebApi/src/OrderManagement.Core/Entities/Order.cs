using OrderManagement.Core.Entities.Abstractions;
using OrderManagement.Core.Enums;

namespace OrderManagement.Core.Entities;

public class Order : BaseEntity
{
    public Order()
    {
        OrderItems = new HashSet<OrderItem>();
        OrderStatus = OrderStatus.Request;
    }

    public Guid OrderId { get; set; } = Guid.NewGuid();
    public string OrderNumber { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public bool IncludeTax { get; set; }
    public OrderStatus OrderStatus { get; set; }

    public ICollection<OrderItem> OrderItems { get; }
}