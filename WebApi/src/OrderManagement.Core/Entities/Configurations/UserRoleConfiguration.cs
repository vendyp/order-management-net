using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderManagement.Core.Entities.Configurations;

public class UserRoleConfiguration : BaseEntityConfiguration<UserRole>
{
    protected override void EntityConfiguration(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(e => e.UserRoleId);
        builder.Property(e => e.UserRoleId).ValueGeneratedNever();
        builder.Property(e => e.RoleId).HasMaxLength(256);
    }
}