using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     ASP.NET Core Identity <see cref="IdentityRoleClaim{TKey}"/> model extension.
/// </summary>
public sealed class RoleClaim : IdentityRoleClaim<long>
{
    /// <summary>
    ///     Private constructor used by Entity Framework Core.
    /// </summary>
    private RoleClaim()
    {
    }
    
    /// <summary>
    ///    Each role can have many claims.
    /// </summary>
    public Role Role { get; set; } = default!;
}