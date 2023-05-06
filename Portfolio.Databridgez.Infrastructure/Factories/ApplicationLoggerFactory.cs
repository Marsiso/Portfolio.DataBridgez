using Microsoft.Extensions.Configuration;
using Serilog.Exceptions;

namespace Portfolio.Databridgez.Infrastructure.Factories;

/// <summary>
///     Creates pre-configured concrete instances of <see cref="ILogger" /> abstraction.
/// </summary>
public static class ApplicationLoggerFactory
{
    /// <summary>
    ///     Gets pre-configured concrete instance of <see cref="ILogger" /> abstraction.
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static ILogger GetLogger(IConfiguration configuration)
    {
        var configurationBuilder = new LoggerConfiguration()
            .OverrideMinimumLoggingLevels()
            .ConfigureLogEventEnrichment()
            .ConfigureLogEventSinks(configuration);

        return configurationBuilder.CreateLogger();
    }

    private static LoggerConfiguration OverrideMinimumLoggingLevels(this LoggerConfiguration configurationBuilder)
    {
        return configurationBuilder
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
    }

    private static LoggerConfiguration ConfigureLogEventEnrichment(this LoggerConfiguration configurationBuilder)
    {
        return configurationBuilder
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

    private static LoggerConfiguration ConfigureLogEventSinks(this LoggerConfiguration configurationBuilder,
        IConfiguration config)
    {
        return configurationBuilder
            .WriteTo.Console(LogEventLevel.Debug,
                outputTemplate: config["Serilog:Sinks:Console:OutputTemplate"] ??
                throw new InvalidOperationException(
                    "Configuration file does not contain definition for console output template"))
            .WriteTo.File(
                path: config["Serilog:Sinks:File:Path"] ??
                throw new InvalidOperationException(
                    "Configuration file does not contain definition for log file destination"),
                LogEventLevel.Warning,
                rollingInterval: RollingInterval.Day,
                outputTemplate: config["Serilog:Sinks:File:OutputTemplate"] ??
                                throw new InvalidOperationException(
                                    "Configuration file does not contain definition for log file output template"))
            .WriteTo.Seq(
                serverUrl: config["Serilog:Sinks:Seq:ServerUrl"] ??
                throw new InvalidOperationException(
                    "Configuration file does not contain definition for Seq server url"),
                LogEventLevel.Debug);
    }
}