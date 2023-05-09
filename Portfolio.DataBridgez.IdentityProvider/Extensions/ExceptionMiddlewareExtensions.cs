using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Portfolio.DataBridgez.IdentityProvider.Middlewares;

namespace Portfolio.DataBridgez.IdentityProvider.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app,
        IWebHostEnvironment env, ILogger logger)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        logger.Error(
                            exceptionHandlerFeature.Error, 
                            "Triggered global exception handler: {Error}",
                            exceptionHandlerFeature.Error);
                        
                        await context.Response.WriteAsync(exceptionHandlerFeature.Error.Message);
                    }
                });
            });
        }
    }
    
    public static void ConfigureExceptionHandler(this IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}