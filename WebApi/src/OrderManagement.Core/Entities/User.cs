using OrderManagement.Core.Entities.Abstractions;

namespace OrderManagement.Core.Entities;

public class User : BaseEntity
{
    public User()
    {
        UserTokens = new HashSet<UserToken>();
        UserRoles = new HashSet<UserRole>();
    }

    public Guid UserId { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = null!;
    public string NormalizedUsername { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string? Salt { get; set; }
    public string? HashedPassword { get; set; }

    public ICollection<UserToken> UserTokens { get; }
    public ICollection<UserRole> UserRoles { get; }
}