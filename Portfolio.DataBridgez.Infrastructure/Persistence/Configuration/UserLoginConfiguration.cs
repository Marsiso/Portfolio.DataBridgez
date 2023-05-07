using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Tables.UserLogins.Columns;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

/// <summary>
///     Configures <see cref="UserLogin"/> mapping to database system.
/// </summary>
public sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable(Tables.UserLogins.TableName, SchemaName);

        builder.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey })
            .IsClustered();
        
        builder.Property(ul => ul.UserId)
            .IsRequired()
            .HasColumnName(UserIdentifierFieldName);

        builder.Property(ul => ul.LoginProvider)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(MaximumLoginProviderLength)
            .HasColumnName(LoginProviderFieldName);
        
        builder.Property(ul => ul.ProviderKey)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(MaximumProviderKeyLength)
            .HasColumnName(ProviderKeyFieldName);
        
        builder.Property(ul => ul.ProviderDisplayName)
            .IsUnicode()
            .IsRequired(false)
            .HasColumnName(ProviderDisplayNameFieldName);
    }
}