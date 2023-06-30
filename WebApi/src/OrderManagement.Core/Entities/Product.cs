using OrderManagement.Core.Entities.Abstractions;

namespace OrderManagement.Core.Entities;

public class Product : BaseEntity
{
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}