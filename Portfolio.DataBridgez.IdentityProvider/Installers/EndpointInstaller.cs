using FluentValidation.AspNetCore;
using Portfolio.DataBridgez.IdentityProvider.Filters;

namespace Portfolio.DataBridgez.IdentityProvider.Installers;

/// <summary>
///     Registers application services related to routing, endpoints and controllers.
/// </summary>
public sealed class EndpointInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddMvc().AddXmlDataContractSerializerFormatters();
        services.AddEndpointsApiExplorer();
        services.AddScoped<RegisterInputValidationFilter>();
        services.AddScoped<RegisterInputCredentialsTakenFilter>();
    }
}