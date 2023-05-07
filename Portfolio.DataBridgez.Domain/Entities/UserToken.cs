using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     User token in database system.
/// </summary>
public sealed class UserToken : IdentityUserToken<long>
{
    /// <summary>
    ///     Used by Entity Framework Core.
    /// </summary>
    private UserToken()
    {
    }

    /// <summary>
    ///     Unique identifier that references <see cref="User"/>.
    /// </summary>
    public override long UserId { get; set; }

    /// <summary>
    ///     Login provider identifier.
    /// </summary>
    public override string LoginProvider { get; set; } = default!;

    /// <summary>
    ///     Token name.
    /// </summary>
    public override string Name { get; set; } = default!;
    
    /// <summary>
    ///     Token value.
    /// </summary>
    public override string? Value { get; set; }

    /// <summary>
    ///     User that token belongs to.
    /// </summary>
    public User User { get; set; } = default!;
}