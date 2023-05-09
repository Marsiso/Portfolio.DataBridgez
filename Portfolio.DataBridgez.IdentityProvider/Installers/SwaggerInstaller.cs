using Microsoft.OpenApi.Models;
using Portfolio.DataBridgez.Domain.Options.Swagger;

namespace Portfolio.DataBridgez.IdentityProvider.Installers;

/// <summary>
///     Registers services related to OpenAPI.
/// </summary>
public sealed class SwaggerInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        var swaggerOptions = new SwaggerOptions();
        config.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(swaggerOptions.Doc.Name, new OpenApiInfo
            {
                Title = swaggerOptions.Doc.OpenApiInfo.Title,
                Version = swaggerOptions.Doc.OpenApiInfo.Version,
                Contact = new OpenApiContact
                {
                    Email = swaggerOptions.Doc.OpenApiInfo.Contact.Email,
                    Name = swaggerOptions.Doc.OpenApiInfo.Contact.Name,
                    Url = new Uri(swaggerOptions.Doc.OpenApiInfo.Contact.Url)
                },
                Description = swaggerOptions.Doc.OpenApiInfo.Description
            });
        });
    }
}