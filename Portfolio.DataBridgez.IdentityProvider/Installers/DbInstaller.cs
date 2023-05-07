﻿using Portfolio.DataBridgez.Infrastructure.Persistence;

namespace Portfolio.DataBridgez.IdentityProvider.Installers;

public sealed class DbInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        ArgumentException.ThrowIfNullOrEmpty(connectionString);
        
        services.AddSqlServer<ApplicationDbContext>(connectionString,
            optionsBuilder => optionsBuilder.MigrationsAssembly("Portfolio.DataBridgez.IdentityProvider"));
    }
}