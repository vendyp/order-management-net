using OrderManagement.Core.Entities;

namespace OrderManagement.Core.Abstractions;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
    bool VerifyPassword(string givenPassword, string salt, string hashedPassword);
    string HashPassword(string password, string salt);
}