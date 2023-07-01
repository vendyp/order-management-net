using Microsoft.EntityFrameworkCore;
using OrderManagement.Core.Abstractions;
using OrderManagement.Core.Entities;

namespace OrderManagement.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IDbContext _dbContext;

    public UserService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<User> GetBaseQuery() => _dbContext.Set<User>()
        .Include(e => e.UserRoles)
        .AsQueryable();

    public Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        => GetBaseQuery().Where(e => e.UserId == userId).FirstOrDefaultAsync(cancellationToken);

    public Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var s = username.ToUpper();
        return GetBaseQuery().Where(e => e.NormalizedUsername == s).FirstOrDefaultAsync(cancellationToken);
    }

    public bool VerifyPassword(string givenPassword, string salt, string hashedPassword)
        => salt + givenPassword == hashedPassword;

    public string HashPassword(string password, string salt)
        => salt + password;
}