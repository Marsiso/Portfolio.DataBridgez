using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Table.UserLogin.Column;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

/// <summary>
///     Configures <see cref="UserLogin"/> mapping to database system.
/// </summary>
public sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable(Table.UserLogin.TableName, SchemaName);

        builder.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey })
            .IsClustered();
        
        builder.Property(ul => ul.UserId)
            .IsRequired()
            .HasColumnName(UserPrimaryKeyColumnName);

        builder.Property(ul => ul.LoginProvider)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(MaximumLoginProviderLengthConstraint)
            .HasColumnName(LoginProviderColumnName);
        
        builder.Property(ul => ul.ProviderKey)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(MaximumProviderKeyLengthConstraint)
            .HasColumnName(ProviderKeyColumnName);
        
        builder.Property(ul => ul.ProviderDisplayName)
            .IsUnicode()
            .IsRequired(false)
            .HasColumnName(ProviderDisplayNameColumnName);
    }
}