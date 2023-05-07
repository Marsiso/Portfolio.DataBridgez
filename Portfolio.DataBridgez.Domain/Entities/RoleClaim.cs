using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     Role claim entity in database system.
/// </summary>
public sealed class RoleClaim : IdentityRoleClaim<long>
{
    /// <summary>
    ///     Unique identifier.
    /// </summary>
    public override int Id { get; set; }
    
    /// <summary>
    ///     Role that claim belongs to identifier.
    /// </summary>
    public override long RoleId { get; set; }
    
    /// <summary>
    ///     Claim type.
    /// </summary>
    public override string? ClaimType { get; set; }
    
    /// <summary>
    ///     Claim value.
    /// </summary>
    public override string? ClaimValue { get; set; }

    /// <summary>
    ///    Role that claim belongs to. 
    /// </summary>
    public Role Role { get; set; } = default!;
}