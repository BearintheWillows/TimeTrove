using System.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Serilog;
using TimeTrove.Client.Pages;
using TimeTrove.Components;
using TimeTrove.Components.Account;
using TimeTrove.Data;
using TimeTrove.Data.Models;
using TimeTrove.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up!");
try
{
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Logging.AddSerilog(dispose: true);
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .WriteTo.MSSqlServer(
            connectionString: builder.Configuration.GetConnectionString("AppDbConnection"),
            tableName: "Logs",
            autoCreateSqlTable: true
            ).MinimumLevel.Warning()
        .Enrich.FromLogContext()
        .WriteTo.Console());
    
    Log.Information("Serilog Configured!");
    
// Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents()
        .AddInteractiveWebAssemblyComponents();

    builder.Services.AddFluentUIComponents();

    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddScoped<IdentityUserAccessor>();
    builder.Services.AddScoped<IdentityRedirectManager>();
    builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

    var connectionString = builder.Configuration.GetConnectionString("AppDbConnection") ??
                           throw new InvalidOperationException("Connection string 'AppDbConnection' not found.");
    
    Log.Information("Received Connection String!");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();
    builder.Services.AddHttpClient();
    builder.Services.AddControllers();
    builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
    builder.Services.AddScoped<IBankAccountService, BankAccountService>();
    
    Log.Information("About to build...!");


    var app = builder.Build();
    
    app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    
    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(Home).Assembly);
        

// Add additional endpoints required by the Identity /Account Razor components.
    app.MapAdditionalIdentityEndpoints();
    app.MapDefaultControllerRoute();
    
    Log.Information("Seeding First User");
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var config = services.GetRequiredService<IConfiguration>();

        // Call the SeedAdmin method
        await SeedAdmin.Initialize(services, userManager, config);
    }

    app.Run();

    Log.Information("Stopped cleanly");
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}