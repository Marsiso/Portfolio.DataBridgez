using Microsoft.AspNetCore.Identity;
using Portfolio.Databridgez.Infrastructure.Identity;

namespace Portfolio.DataBridgez.IdentityProvider.Installers;

public sealed class IdentityInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<AppUser>();
        services.AddScoped<IdentityStore>();
        services.AddScoped<IPasswordHasher<AppUser>, Pbkdf2PasswordHasher>();
        services.AddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
        services.AddScoped<IUserStore<AppUser>, IdentityStore>();
        services.AddScoped<IUserPasswordStore<AppUser>, IdentityStore>();
        services.AddScoped<IUserLockoutStore<AppUser>, IdentityStore>();
        services.AddScoped<IProtectedUserStore<AppUser>, IdentityStore>();
        services.AddScoped<IQueryableUserStore<AppUser>, IdentityStore>();
        services.AddScoped<IUserEmailStore<AppUser>, IdentityStore>();
        services.AddScoped<IUserSecurityStampStore<AppUser>, IdentityStore>();
    }
}