using Portfolio.DataBridgez.Domain.Options.Swagger;

namespace Portfolio.DataBridgez.IdentityProvider.Extensions;

public static class SwaggerMiddlewareExtensions
{
    public static void ConfigureSwagger(this IApplicationBuilder app,
        IConfiguration config, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            var swaggerOptions = new SwaggerOptions();
            config.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
        
            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions.RouteTemplate;
            });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });
        }
    }
}