using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Table.Role.Column;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

/// <summary>
///     Configures <see cref="Role"/> mapping to database system.
/// </summary>
public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(Table.Role.TableName, SchemaName);

        builder.HasKey(r => r.Id)
            .IsClustered();

        builder.HasIndex(r => r.NormalizedName)
            .IsUnique()
            .HasDatabaseName("IX_role_normalized_name");
        
        builder.Property(r => r.Id)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName(PrimaryKeyColumnName);
        
        builder.Property(r => r.Name)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnName(NameColumnName)
            .HasMaxLength(MaximumNameLengthConstraint);
        
        builder.Property(r => r.NormalizedName)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnName(NormalizedNameColumnName)
            .HasMaxLength(MaximumNameLengthConstraint);

        builder.Property(r => r.ConcurrencyStamp)
            .IsRequired(false)
            .HasColumnName(ConcurrencyStampColumnName)
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