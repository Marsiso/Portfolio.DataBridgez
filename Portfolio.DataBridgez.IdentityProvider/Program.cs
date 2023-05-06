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
        app.UseSwagger();
        app.UseSwaggerUI();
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