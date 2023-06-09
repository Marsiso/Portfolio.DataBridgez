﻿using Portfolio.DataBridgez.IdentityProvider.Middlewares;

namespace Portfolio.DataBridgez.IdentityProvider.Installers;

public sealed class MiddlewareInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<ExceptionMiddleware>();
    }
}