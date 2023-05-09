using Portfolio.DataBridgez.Infrastructure.Persistence;
using Portfolio.DataBridgez.Infrastructure.Persistence.Repositories;

namespace Portfolio.DataBridgez.IdentityProvider.Installers;

/// <summary>
///     Registers application services related to persistence.
/// </summary>
public sealed class DbInstaller : IInstaller
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        ArgumentException.ThrowIfNullOrEmpty(connectionString);
        
        services.AddSqlServer<AppDbContext>(connectionString,
            optionsBuilder => optionsBuilder.MigrationsAssembly("Portfolio.DataBridgez.IdentityProvider"));

        services.AddScoped<IRepository<AppUser>, Repository<AppUser>>();
    }
}