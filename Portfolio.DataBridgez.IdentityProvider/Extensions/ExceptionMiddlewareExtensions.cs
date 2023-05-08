using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Portfolio.DataBridgez.IdentityProvider.Middlewares;

namespace Portfolio.DataBridgez.IdentityProvider.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder applicationBuilder,
        IWebHostEnvironment environment, ILogger logger)
    {
        if (environment.IsDevelopment())
        {
            applicationBuilder.UseDeveloperExceptionPage();
        }
        else
        {
            applicationBuilder.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    
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
    
    public static void ConfigureExceptionHandler(this IApplicationBuilder applicationBuilder,
        IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            applicationBuilder.UseDeveloperExceptionPage();
        }
        else
        {
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}