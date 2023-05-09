using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Portfolio.DataBridgez.Application.Interfaces;

/// <summary>
///     Provides an abstraction for service registration.
/// </summary>
public interface IInstaller
{
    /// <summary>
    ///     Registers service into the service collection.
    /// </summary>
    /// <param name="services">An abstraction for the service collection.</param>
    /// <param name="config">An abstraction for configuration properties.</param>
    void RegisterServices(IServiceCollection services, IConfiguration config);
}