using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     User in the database system.
/// </summary>
public sealed class User : IdentityUser<long>
{
    /// <summary>
    ///     Private constructor used by Entity Framework Core.
    /// </summary>
    private User()
    {
    }
    
    /// <summary>
    ///     User unique identifier.
    /// </summary>
    public override long Id { get; set; }
    
    /// <summary>
    ///     User unique nick name used as log in option.
    /// </summary>
    public override string? UserName { get; set; }
    
    /// <summary>
    ///     Normalized user name.
    /// </summary>
    public override string? NormalizedUserName { get; set; }
    
    /// <summary>
    ///     User unique email address used as log in option.
    /// </summary>
    public override string? Email { get; set; }
    
    /// <summary>
    ///     Normalized user email.
    /// </summary>
    public override string? NormalizedEmail { get; set; }
    
    /// <summary>
    ///     Flag that indicates whether user confirmed email address.
    /// </summary>
    public override bool EmailConfirmed { get; set; }
    
    /// <summary>
    ///     User password hash computed using <see cref="IPasswordHasher{TUser}"/> abstraction.
    /// </summary>
    public override string? PasswordHash { get; set; }
    
    /// <summary>
    ///     Current snapshot of user credentials including external logins etc.
    /// </summary>
    public override string? SecurityStamp { get; set; }
    
    /// <summary>
    ///     Prevents concurrency update conflict.
    /// </summary>
    public override string? ConcurrencyStamp { get; set; }
    
    /// <summary>
    ///     User unique phone number.
    /// </summary>
    public override string? PhoneNumber { get; set; }
    
    /// <summary>
    ///     Flag that indicates whether user has confirmed phone number.
    /// </summary>
    public override bool PhoneNumberConfirmed { get; set; }
    
    /// <summary>
    ///     Flag that indicates whether user two factor authentication is enabled.
    /// </summary>
    public override bool TwoFactorEnabled { get; set; }
    
    /// <summary>
    ///     Expiration date for user lock out.
    /// </summary>
    public override DateTimeOffset? LockoutEnd { get; set; }
    
    /// <summary>
    ///     Flag that indicates whether user lock out is enabled.
    ///     User is locked out after certain amount of failed log in attempts.
    /// </summary>
    public override bool LockoutEnabled { get; set; }
    
    /// <summary>
    ///     Failed attempts to log in counter. Triggers user lock out after reaching certain amount.
    /// </summary>
    public override int AccessFailedCount { get; set; }

    /// <summary>
    ///     Roles that have been assigned to user.
    /// </summary>
    public ICollection<UserRole> Roles { get; set; } = default!;

    /// <summary>
    ///     Tokens that have been issued.
    /// </summary>
    public ICollection<UserToken> Tokens { get; set; } = default!;

    /// <summary>
    ///     Claims that belong to user.
    /// </summary>
    public ICollection<UserClaim> Claims { get; set; } = default!;

    /// <summary>
    ///     Persisted user log ins.
    /// </summary>
    public ICollection<UserLogin> Logins { get; set; } = default!;
}