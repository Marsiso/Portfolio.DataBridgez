using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     ASP.NET Core Identity <see cref="IdentityUserClaim{TKey}"/> model extension.
/// </summary>
public sealed class UserClaim : IdentityUserClaim<long>
{
    /// <summary>
    ///     Private constructor used by Entity Framework Core.
    /// </summary>
    private UserClaim()
    {
    }
    
    /// <summary>
    ///     Each user can have many claims.
    /// </summary>
    public User User { get; set; } = default!;
}