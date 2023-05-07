using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     ASP.NET Core Identity <see cref="IdentityUserToken{TKey}"/> model extension.
/// </summary>
public sealed class UserToken : IdentityUserToken<long>
{
    /// <summary>
    ///     Private constructor used by Entity Framework Core.
    /// </summary>
    private UserToken()
    {
    }

    /// <summary>
    ///     Each user can have many tokens.
    /// </summary>
    public User User { get; set; } = default!;
}