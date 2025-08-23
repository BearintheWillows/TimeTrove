using Serilog;
using TimeTrove.Infrastructure.Data.Context;

namespace TimeTrove.Api.Middleware;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ApplicationServicesMiddleware(this IServiceCollection services, IConfiguration config)
    {
        services.AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(config)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());

        Log.Information("Adding services");
        services.AddControllers();
        services.AddOpenApi();

        services.AddDbContext<ApplicationDbContext>(options => config.GetConnectionString("DefaultConnection"));

        return services;
    }
}