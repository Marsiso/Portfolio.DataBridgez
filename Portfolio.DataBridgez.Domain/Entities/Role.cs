using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     User role in database system.
/// </summary>
public sealed class Role : IdentityRole<long>
{
    /// <summary>
    ///     Used by Entity Framework Core.
    /// </summary>
    private Role()
    {
    }
    
    /// <summary>
    ///     Unique identifier.
    /// </summary>
    public override long Id { get; set; }
    
    /// <summary>
    ///     Unique role name.
    /// </summary>
    public override string? Name { get; set; }
    
    /// <summary>
    ///     Normalized name.
    /// </summary>
    public override string? NormalizedName { get; set; }
    
    /// <summary>
    ///     Prevents concurrency update conflict.
    /// </summary>
    public override string? ConcurrencyStamp { get; set; }

    /// <summary>
    ///     Users that role has been assigned to.
    /// </summary>
    public ICollection<UserRole> Users { get; set; } = default!;

    /// <summary>
    ///     Claims associated with role.
    /// </summary>
    public ICollection<RoleClaim> Claims { get; set; } = default!;
}