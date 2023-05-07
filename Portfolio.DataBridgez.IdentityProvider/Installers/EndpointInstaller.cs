namespace Portfolio.DataBridgez.IdentityProvider.Installers;

/// <summary>
///     Registers application services related to routing, endpoints and controllers.
/// </summary>
public sealed class EndpointInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMvc();
        services.AddEndpointsApiExplorer();
    }
}