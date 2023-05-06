using Portfolio.Databridgez.Application.Interfaces;

namespace Portfolio.DataBridgez.IdentityProvider.Installers;

public sealed class SwaggerInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen();
    }
}