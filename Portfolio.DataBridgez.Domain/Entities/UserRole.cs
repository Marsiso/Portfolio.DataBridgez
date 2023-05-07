using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     ASP.NET Core Identity <see cref="IdentityUserRole{TKey}"/> model extension.
/// </summary>
public sealed class UserRole : IdentityUserRole<long>
{
    /// <summary>
    ///     Private constructor used by Entity Framework Core.
    /// </summary>
    private UserRole()
    {
    }

    /// <summary>
    ///     Each user can have many roles.
    /// </summary>
    public User User { get; set; } = default!;

    /// <summary>
    ///     Each role can have many users.
    /// </summary>
    public Role Role { get; set; } = default!;
}