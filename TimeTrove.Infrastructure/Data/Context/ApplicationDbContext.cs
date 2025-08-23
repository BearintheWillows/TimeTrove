using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimeTrove.Domain.Models;

namespace TimeTrove.Infrastructure.Data.Context;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Account> Accounts { get; set; }
}