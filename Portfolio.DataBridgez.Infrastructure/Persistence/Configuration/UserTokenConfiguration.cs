using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Table.UserToken.Column;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

/// <summary>
///     Configures <see cref="UserToken"/> mapping to database system.
/// </summary>
public sealed class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(Table.UserToken.TableName, SchemaName);

        builder.HasKey(ut => new { ut.LoginProvider, ut.UserId, ut.Name })
            .IsClustered();

        builder.Property(ut => ut.UserId)
            .IsRequired()
            .HasColumnName(UserPrimaryKeyColumnName);

        builder.Property(ut => ut.Name)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(MaximumNameLengthConstraint)
            .HasColumnName(NameColumnName);
        
        builder.Property(ut => ut.LoginProvider)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(MaximumLoginProviderLengthConstraint)
            .HasColumnName(LoginProviderColumnName);
        
        builder.Property(ut => ut.Value)
            .IsUnicode()
            .IsRequired(false)
            .HasColumnName(ValueColumnName);
    }
}