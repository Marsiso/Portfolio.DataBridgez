using Microsoft.Extensions.Configuration;
using Portfolio.DataBridgez.Domain.Options;
using Portfolio.DataBridgez.Domain.Options.Serilog;
using Serilog.Exceptions;

namespace Portfolio.DataBridgez.Infrastructure.Factories;

/// <summary>
///     Creates pre-configured instances of <see cref="ILogger" />.
/// </summary>
public static class AppLoggerFactory
{
    /// <summary>
    ///     Gets the pre-configured instance of <see cref="ILogger" />.
    /// </summary>
    /// <param name="config">An abstraction for configuration properties.</param>
    /// <returns></returns>
    public static ILogger GetLogger(IConfiguration config)
    {
        var configBuilder = new LoggerConfiguration()
            .OverrideMinimumLoggingLevels()
            .ConfigureLogEventEnrichment()
            .ConfigureLogEventSinks(config);

        return configBuilder.CreateLogger();
    }

    private static LoggerConfiguration OverrideMinimumLoggingLevels(this LoggerConfiguration configBuilder)
    {
        return configBuilder
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
    }

    private static LoggerConfiguration ConfigureLogEventEnrichment(this LoggerConfiguration configBuilder)
    {
        return configBuilder
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.WithMemoryUsage()
            .Enrich.WithMemoryUsage()
            .Enrich.WithExceptionDetails()
            .Enrich.FromLogContext();
    }

    private static LoggerConfiguration ConfigureLogEventSinks(this LoggerConfiguration configBuilder,
        IConfiguration config)
    {
        var options = new SerilogOptions();
        config.GetSection(nameof(SerilogOptions)).Bind(options);
        
        return configBuilder
            .WriteTo.Console(LogEventLevel.Debug,
                outputTemplate: options.Console.OutputTemplate)
            .WriteTo.File(
                path: options.File.Path,
                restrictedToMinimumLevel: LogEventLevel.Warning,
                rollingInterval: RollingInterval.Day,
                outputTemplate: options.File.OutputTemplate)
            .WriteTo.Seq(
                serverUrl: options.Seq.ServerUrl,
                restrictedToMinimumLevel: LogEventLevel.Debug);
    }
}