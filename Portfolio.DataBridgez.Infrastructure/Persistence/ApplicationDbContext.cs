using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataBridgez.Domain.Entities;
using Portfolio.DataBridgez.Infrastructure.Persistence.Configuration;

namespace Portfolio.DataBridgez.Infrastructure.Persistence;

/// <summary>
///     Application database context that extends ASP.NET Core Identity
///     <see cref="IdentityDbContext{TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken}"/>.
///     context.
/// </summary>
public sealed class ApplicationDbContext : IdentityDbContext<
    User,
    Role,
    long,
    UserClaim,
    UserRole,
    UserLogin,
    RoleClaim,
    UserToken>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserClaimConfiguration());
        builder.ApplyConfiguration(new UserLoginConfiguration());
        builder.ApplyConfiguration(new UserTokenConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new RoleClaimConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
    }
}