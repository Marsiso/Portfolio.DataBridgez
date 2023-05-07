﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.DataBridgez.Domain.Entities;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.DbDefaults.Tables.Users.Columns;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

/// <summary>
///     Configures <see cref="User"/> mapping to database system.
/// </summary>
public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(Tables.Users.TableName, SchemaName);

        builder.HasKey(u => u.Id)
            .IsClustered();
        
        builder.HasIndex(u => u.NormalizedUserName)
            .IsUnique()
            .HasDatabaseName("IX_user_normalized_user_name");

        builder.HasIndex(u => u.NormalizedEmail)
            .IsUnique()
            .HasDatabaseName("IX_user_normalized_email");

        builder.Property(u => u.Id)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName(IdentifierFieldName);

        builder.Property(u => u.UserName)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnName(UserNameFieldName)
            .HasMaxLength(MaximumUserNameLength);
        
        builder.Property(u => u.NormalizedUserName)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnName(NormalizedUserNameFieldName)
            .HasMaxLength(MaximumUserNameLength);
        
        builder.Property(u => u.Email)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnName(EmailFieldName)
            .HasMaxLength(MaximumEmailLength);
        
        builder.Property(u => u.NormalizedEmail)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnName(NormalizedEmailFieldName)
            .HasMaxLength(MaximumEmailLength);

        builder.Property(u => u.EmailConfirmed)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName(EmailConfirmedFieldName);
        
        builder.Property(u => u.PasswordHash)
            .IsUnicode()
            .HasColumnName(PasswordHashFieldName);
        
        builder.Property(u => u.ConcurrencyStamp)
            .IsRequired(false)
            .IsUnicode()
            .IsConcurrencyToken()
            .HasColumnName(ConcurrencyStampFieldName);
        
        builder.Property(u => u.SecurityStamp)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnName(SecurityStampFieldName);
        
        builder.Property(u => u.PhoneNumber)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnName(PhoneNumberFieldName);
        
        builder.Property(u => u.PhoneNumberConfirmed)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName(PhoneNumberConfirmedFieldName);
        
        builder.Property(u => u.TwoFactorEnabled)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName(TwoFactorEnabledFieldName);
        
        builder.Property(u => u.LockoutEnd)
            .IsRequired(false)
            .HasColumnName(LockOutEndFieldName);
        
        builder.Property(u => u.LockoutEnabled)
            .IsRequired()
            .HasDefaultValue(true)
            .HasColumnName(LockOutEnabledFieldName);
        
        builder.Property(u => u.AccessFailedCount)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnName(AccessFailedCountFieldName);
        
        builder.HasMany(u => u.Roles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Tokens)
            .WithOne(ut => ut.User)
            .HasForeignKey(ut => ut.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(u => u.Claims)
            .WithOne(uc => uc.User)
            .HasForeignKey(uc => uc.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(u => u.Logins)
            .WithOne(ul => ul.User)
            .HasForeignKey(ul => ul.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}