using Scalar.AspNetCore;
using Serilog;
using TimeTrove.Api.Middleware;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Startup/Logs/log-.txt")
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.ApplicationServicesMiddleware(builder.Configuration);

    Log.Information("Building app");
    var app = builder.Build();

    Log.Information("Configuring app");
    if (app.Environment.IsDevelopment())
    {
        app.MapScalarApiReference();
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseMiddleware<CorrelationIdMiddleware>();
    app.UseSerilogRequestLogging();

    app.UseAuthorization();

    app.MapControllers();

    await app.RunAsync();

    Log.Information("Shutting down");
    return 0;
}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}