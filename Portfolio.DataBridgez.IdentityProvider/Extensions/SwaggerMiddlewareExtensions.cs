using Portfolio.DataBridgez.Domain.Options.Swagger;

namespace Portfolio.DataBridgez.IdentityProvider.Extensions;

public static class SwaggerMiddlewareExtensions
{
    public static void ConfigureSwagger(this IApplicationBuilder applicationBuilder,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            var swaggerOptions = new SwaggerOptions();
            configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
        
            applicationBuilder.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions.RouteTemplate;
            });
            applicationBuilder.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });
        }
    }
}