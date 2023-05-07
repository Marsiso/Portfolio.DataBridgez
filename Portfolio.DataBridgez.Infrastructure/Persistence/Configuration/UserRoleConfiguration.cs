using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Table.UserRole.Column;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(Table.UserRole.TableName, SchemaName);

        builder.Property(ur => ur.UserId)
            .IsRequired()
            .HasColumnName(UserPrimaryKeyColumnName);
        
        builder.Property(ur => ur.RoleId)
            .IsRequired()
            .HasColumnName(RolePrimaryKeyColumnName);
    }
}