using Microsoft.Extensions.Configuration;
using Portfolio.Databridgez.Domain.Options;
using Portfolio.Databridgez.Domain.Options.Serilog;
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
        IConfiguration configuration)
    {
        var serilogOptions = new SerilogSinkOptions();
        configuration.GetSection(nameof(SerilogSinkOptions)).Bind(serilogOptions);
        
        return configurationBuilder
            .WriteTo.Console(LogEventLevel.Debug,
                outputTemplate: serilogOptions.Console.OutputTemplate)
            .WriteTo.File(
                path: serilogOptions.File.Path,
                LogEventLevel.Warning,
                rollingInterval: RollingInterval.Day,
                outputTemplate: serilogOptions.File.OutputTemplate)
            .WriteTo.Seq(
                serverUrl: serilogOptions.Seq.ServerUrl,
                LogEventLevel.Debug);
    }
}