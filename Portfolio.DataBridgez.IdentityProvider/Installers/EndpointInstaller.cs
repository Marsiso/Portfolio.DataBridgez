﻿using Portfolio.Databridgez.Application.Interfaces;

namespace Portfolio.DataBridgez.IdentityProvider.Installers;

public sealed class EndpointInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
    }
}