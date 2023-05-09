using Portfolio.Databridgez.Domain.Options.Jwt;
using Portfolio.DataBridgez.IdentityProvider.Services;

namespace Portfolio.DataBridgez.IdentityProvider.Installers;

public sealed class JwtInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IAccessTokenProvider, JwtAccessTokenProvider>();
    }
}