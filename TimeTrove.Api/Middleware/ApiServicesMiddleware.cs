using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TimeTrove.Infrastructure.Data.Context;

namespace TimeTrove.Api.Middleware;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ApplicationServicesMiddleware(this IServiceCollection services, IConfiguration config)
    {
        services.AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(config)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());

        Log.Information("Adding services");

        services.AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddControllers();
        services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
                };
            });

        services.AddDbContext<ApplicationDbContext>(options => config.GetConnectionString("DefaultConnection"));

        services.AddCors(options =>
        {
            options.AddPolicy("AllowVueApp", policy =>
            {
                policy.WithOrigins("http://localhost:5173") // Vue dev server
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddOpenApi();

        return services;
    }
}