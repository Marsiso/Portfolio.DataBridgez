using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.DataBridgez.Domain.Entities;

/// <summary>
///     User claim in database system.
/// </summary>
public sealed class UserClaim : IdentityUserClaim<long>
{
    public override int Id { get; set; }

    /// <summary>
    ///     Claim type.
    /// </summary>
    public override string? ClaimType { get; set; }

    /// <summary>
    ///     Claim value.
    /// </summary>
    public override string? ClaimValue { get; set; }
    
    /// <summary>
    ///     User that claim belongs to identifier.
    /// </summary>
    public override long UserId { get; set; }

    /// <summary>
    ///     User that claim belongs to.
    /// </summary>
    public User User { get; set; } = default!;
}