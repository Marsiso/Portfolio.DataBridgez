using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Portfolio.DataBridgez.Application.Interfaces;

/// <summary>
///     Provides an abstraction for service registration.
/// </summary>
public interface IInstaller
{
    /// <summary>
    ///     Registers service into application service collection.
    /// </summary>
    /// <param name="services">Application abstraction for service collection.</param>
    /// <param name="configuration">Application abstraction for configuration properties.</param>
    public void RegisterServices(IServiceCollection services, IConfiguration configuration);
}