using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Portfolio.DataBridgez.Domain.Constants.AppDbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.AppDbDefaults.Tables.AppUsers.Column;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

/// <summary>
///     The <see cref="AppUser"/> database mapping configuration.
/// </summary>
public sealed class AppUserConfig : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(u => u.Id)
            .IsClustered();
    }
}