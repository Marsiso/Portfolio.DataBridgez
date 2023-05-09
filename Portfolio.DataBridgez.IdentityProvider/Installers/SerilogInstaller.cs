namespace Portfolio.DataBridgez.IdentityProvider.Installers;

public sealed class SerilogInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton(Log.Logger);
    }
}