using Portfolio.Databridgez.Domain.Options;
using Portfolio.Databridgez.Domain.Options.Swagger;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = ApplicationLoggerFactory.GetLogger(configuration);

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();
    builder.Services.InstallServicesInAssembly(builder.Configuration);

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    
    if (app.Environment.IsDevelopment())
    {
        var swaggerOptions = new SwaggerOptions();
        app.Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
        
        app.UseSwagger(option =>
        {
            option.RouteTemplate = swaggerOptions.RouteTemplate;
        });
        app.UseSwaggerUI(option =>
        {
            option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
        });
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    Log.Information("Identity provider application is starting up");
    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Identity provider application application failed to start up");
}
finally
{
    Log.CloseAndFlush();
}