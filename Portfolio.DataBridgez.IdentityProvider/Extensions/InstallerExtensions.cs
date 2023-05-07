namespace Portfolio.DataBridgez.IdentityProvider.Extensions;

/// <summary>
///     <see cref="IInstaller" /> abstraction extension methods.
/// </summary>
public static class InstallerExtensions
{
    /// <summary>
    ///     Initializes and executes every implementation of <see cref="IInstaller" /> in assembly.
    /// </summary>
    /// <param name="services">Application abstraction for collection of services.</param>
    /// <param name="configuration">Application abstraction for configuration properties.</param>
    public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
    {
        var installers = typeof(Program).Assembly.ExportedTypes
            .Where(exportedType => typeof(IInstaller).IsAssignableFrom(exportedType) &&
                                   exportedType is { IsAbstract: false, IsInterface: false })
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .ToList();

        installers.ForEach(installer => installer.RegisterServices(services, configuration));
    }
}