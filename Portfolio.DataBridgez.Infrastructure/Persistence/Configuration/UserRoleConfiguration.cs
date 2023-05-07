using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Tables.UserRoles.Columns;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(Tables.UserRoles.TableName, SchemaName);

        builder.Property(ur => ur.UserId)
            .IsRequired()
            .HasColumnName(UserIdentifierFieldName);
        
        builder.Property(ur => ur.RoleId)
            .IsRequired()
            .HasColumnName(RoleIdentifierFieldName);
    }
}