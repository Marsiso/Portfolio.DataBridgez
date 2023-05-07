using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     ASP.NET Core Identity <see cref="IdentityUser{TKey}"/> model extension.
/// </summary>
public sealed class User : IdentityUser<long>
{
    /// <summary>
    ///     Parameterless constructor.
    /// </summary>
    public User()
    {
    }
    
    /// <summary>
    ///     Initializes new instance of <see cref="User"/>.
    /// </summary>
    /// <param name="userName">
    ///     Application <see cref="User"/> property <see cref="IdentityUser{TKey}.UserName"/>.
    /// </param>
    public User(string userName) : base(userName)
    {
    }

    /// <summary>
    ///     Each user can have many roles.
    /// </summary>
    public ICollection<UserRole> Roles { get; set; } = default!;

    /// <summary>
    ///     Each user can have many tokens.
    /// </summary>
    public ICollection<UserToken> Tokens { get; set; } = default!;

    /// <summary>
    ///     Each user can have many claims.
    /// </summary>
    public ICollection<UserClaim> Claims { get; set; } = default!;

    /// <summary>
    ///     Each user can have many logins.
    /// </summary>
    public ICollection<UserLogin> Logins { get; set; } = default!;
}