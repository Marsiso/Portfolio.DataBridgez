using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     ASP.NET Core Identity <see cref="IdentityUserLogin{TKey}"/> model extension.
/// </summary>
public sealed class UserLogin : IdentityUserLogin<long>
{
    /// <summary>
    ///     Private constructor used by Entity Framework Core.
    /// </summary>
    private UserLogin()
    {
    }

    /// <summary>
    ///     Each user can have many logins.
    /// </summary>
    public User User { get; set; } = default!;
}