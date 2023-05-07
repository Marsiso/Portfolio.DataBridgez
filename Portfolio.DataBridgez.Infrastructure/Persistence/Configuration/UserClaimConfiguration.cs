using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Table.UserClaim.Columns;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

/// <summary>
///     Configures <see cref="UserClaim"/> mapping to database system.
/// </summary>
public sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable(Table.UserClaim.TableName, SchemaName);
        
        builder.HasKey(uc => uc.Id)
            .IsClustered();
        
        builder.Property(uc => uc.Id)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName(PrimaryKeyColumnName);

        builder.Property(uc => uc.ClaimType)
            .IsUnicode()
            .IsRequired(false)
            .HasColumnName(ClaimTypeColumnName);
        
        builder.Property(uc => uc.ClaimValue)
            .IsUnicode()
            .IsRequired(false)
            .HasColumnName(ClaimValueColumnName);

        builder.Property(uc => uc.UserId)
            .IsRequired()
            .HasColumnName(UserPrimaryKeyColumnName);
    }
}