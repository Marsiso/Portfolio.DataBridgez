using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     ASP.NET Core Identity <see cref="IdentityRole{TKey}"/> model extension.
/// </summary>
public sealed class Role : IdentityRole<long>
{
    /// <summary>
    ///     Private constructor used by Entity Framework Core.
    /// </summary>
    private Role()
    {
    }

    /// <summary>
    ///     Each role can have many users.
    /// </summary>
    public ICollection<UserRole> Users { get; set; } = default!;

    /// <summary>
    ///     Each role can have many claims.
    /// </summary>
    public ICollection<RoleClaim> Claims { get; set; } = default!;
}