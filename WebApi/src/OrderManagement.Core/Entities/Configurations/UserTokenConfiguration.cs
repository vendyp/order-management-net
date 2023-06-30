using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderManagement.Core.Entities.Configurations;

public class UserTokenConfiguration : BaseEntityConfiguration<UserToken>
{
    protected override void EntityConfiguration(EntityTypeBuilder<UserToken> builder)
    {
        throw new NotImplementedException();
    }
}