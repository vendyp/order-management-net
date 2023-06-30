using OrderManagement.Core.Entities.Abstractions;

namespace OrderManagement.Core.Entities;

public class UserRole : BaseEntity
{
    public Guid UserRoleId { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string RoleId { get; set; } = null!;
}