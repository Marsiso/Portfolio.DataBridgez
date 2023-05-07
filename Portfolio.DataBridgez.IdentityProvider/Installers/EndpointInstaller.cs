namespace Portfolio.DataBridgez.IdentityProvider.Installers;

public sealed class EndpointInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMvc();
        services.AddEndpointsApiExplorer();
    }
}