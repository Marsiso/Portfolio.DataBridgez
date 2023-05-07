using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Tables.Roles.Columns;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

/// <summary>
///     Configures <see cref="Role"/> mapping to database system.
/// </summary>
public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(Tables.Roles.TableName, SchemaName);

        builder.HasKey(r => r.Id)
            .IsClustered();

        builder.HasIndex(r => r.NormalizedName)
            .IsUnique()
            .HasDatabaseName("IX_role_normalized_name");
        
        builder.Property(r => r.Id)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName(IdentifierFieldName);
        
        builder.Property(r => r.Name)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnName(NameFieldName)
            .HasMaxLength(MaximumNameLength);
        
        builder.Property(r => r.NormalizedName)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnName(NormalizedNameFieldName)
            .HasMaxLength(MaximumNameLength);

        builder.Property(r => r.ConcurrencyStamp)
            .IsRequired(false)
            .HasColumnName(ConcurrencyStampFieldName)
            .IsConcurrencyToken();

        builder.HasMany(r => r.Users)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(r => r.Claims)
            .WithOne(rc => rc.Role)
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}