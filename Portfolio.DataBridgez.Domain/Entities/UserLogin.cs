using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     User login in database system.
/// </summary>
public sealed class UserLogin : IdentityUserLogin<long>
{
    /// <summary>
    ///     Used by Entity Framework Core.
    /// </summary>
    private UserLogin()
    {
    }

    /// <summary>
    ///     Login provider identifier.
    /// </summary>
    public override string LoginProvider { get; set; } = default!;

    public override string ProviderKey { get; set; } = default!;
   
    /// <summary>
    ///     Login provider display name.
    /// </summary>
    public override string? ProviderDisplayName { get; set; }
    
    /// <summary>
    ///     User that login provider is bound to identifier.
    /// </summary>
    public override long UserId { get; set; }

    /// <summary>
    ///     User that login provider is bound to.
    /// </summary>
    public User User { get; set; } = default!;
}