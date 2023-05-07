using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Tables.RoleClaims.Columns;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

/// <summary>
///     Configures <see cref="RoleClaim"/> mapping to database system.
/// </summary>
public sealed class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable(Tables.RoleClaims.TableName, SchemaName);

        builder.HasKey(rc => rc.Id)
            .IsClustered();

        builder.Property(rc => rc.Id)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName(IdentifierFieldName);

        builder.Property(rc => rc.RoleId)
            .IsRequired()
            .HasColumnName(RoleIdentifierFieldName);

        builder.Property(rc => rc.ClaimType)
            .IsUnicode()
            .IsRequired(false)
            .HasColumnName(ClaimTypeFieldName);
        
        builder.Property(rc => rc.ClaimValue)
            .IsUnicode()
            .IsRequired(false)
            .HasColumnName(ClaimValueFieldName);
    }
}