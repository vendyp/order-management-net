using OrderManagement.Core.Entities.Abstractions;

namespace OrderManagement.Core.Entities;

public class UserToken : BaseEntity
{
    public Guid UserTokenId { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; } = null!;
    public bool IsUsed { get; set; }
    public DateTime? UsedAt { get; set; }
}