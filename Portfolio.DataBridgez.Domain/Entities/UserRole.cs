using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     <see cref="User"/> and <see cref="Role"/> many-to-many relationship in database system.
/// </summary>
public sealed class UserRole : IdentityUserRole<long>
{
    /// <summary>
    ///     Used by Entity Framework Core
    /// </summary>
    private UserRole()
    {
    }
    
    /// <summary>
    ///     Unique identifier that references <see cref="User"/>.
    /// </summary>
    public override long UserId { get; set; }
    
    /// <summary>
    ///     Unique identifier that references <see cref="Role"/>.
    /// </summary>
    public override long RoleId { get; set; }

    /// <summary>
    ///     User in role.
    /// </summary>
    public User User { get; set; } = default!;

    /// <summary>
    ///     Role assigned to user.
    /// </summary>
    public Role Role { get; set; } = default!;
}