using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace Portfolio.DataBridgez.IdentityProvider.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder applicationBuilder,
        IWebHostEnvironment environment)
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
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        await context.Response.WriteAsync(exceptionHandlerFeature.Error.Message);
                    }
                });
            });
        }
    }
}