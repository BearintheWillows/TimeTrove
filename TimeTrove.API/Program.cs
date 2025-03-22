using TimeTrove.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<DatabaseWrapper>(provider => 
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        return new DatabaseWrapper(connectionString);
    });

builder.Services.AddScoped<IBaseRepository, BaseRepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();