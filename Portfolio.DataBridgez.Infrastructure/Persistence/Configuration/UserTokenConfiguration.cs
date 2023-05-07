using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Tables.UserTokens.Columns;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

/// <summary>
///     Configures <see cref="UserToken"/> mapping to database system.
/// </summary>
public sealed class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(Tables.UserTokens.TableName, SchemaName);

        builder.HasKey(ut => new { ut.LoginProvider, ut.UserId, ut.Name })
            .IsClustered();

        builder.Property(ut => ut.UserId)
            .IsRequired()
            .HasColumnName(UserIdentifierFieldName);

        builder.Property(ut => ut.Name)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(MaximumNameLength)
            .HasColumnName(NameFieldName);
        
        builder.Property(ut => ut.LoginProvider)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(MaximumLoginProviderLength)
            .HasColumnName(LoginProviderFieldName);
        
        builder.Property(ut => ut.Value)
            .IsUnicode()
            .IsRequired(false)
            .HasColumnName(ValueFieldName);
    }
}