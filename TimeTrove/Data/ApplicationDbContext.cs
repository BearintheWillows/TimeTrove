using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TimeTrove.Client.Models.Shared;
using TimeTrove.Data.Configs;
using TimeTrove.Data.Interfaces;
using TimeTrove.Data.Models;


namespace TimeTrove.Data;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {  
        ChangeTracker.Tracked += UpdateTimestamps;
        ChangeTracker.StateChanged += UpdateTimestamps;
    }
    
    public virtual DbSet<BankAccount> BankAccounts { get; set; } 
    public virtual DbSet<Budget> Budgets { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new RoleConfiguration());
        
      
        builder.Entity<Transaction>()
            .HasOne(t => t.PrincipleBankAccount)
            .WithMany(b => b.Transactions)
            .HasForeignKey(t => t.PrincipleBankAccountId);

        builder.Entity<Transaction>()
            .HasOne(t => t.SecondaryBankAccount)
            .WithMany() // You may not want this to be part of transactions collection.
            .HasForeignKey(t => t.SecondaryBankAccountId)
            .OnDelete(DeleteBehavior.Restrict); // prevent cascade delete
        
        
    }
    
    private static void UpdateTimestamps(object sender, EntityEntryEventArgs e)
    {
        if (e.Entry.Entity is IHasTimestamps entityWithTimestamps)
            switch (e.Entry.State)
            {
                case EntityState.Deleted:
                    entityWithTimestamps.Deleted = DateTime.UtcNow;
                    /*Log.Information($"Stamped for delete: {e.Entry.Entity}");*/
                    break;
                case EntityState.Modified:
                    entityWithTimestamps.Modified = DateTime.UtcNow;
                    /*Log.Information($"Stamped for update: {e.Entry.Entity}");*/
                    break;
                case EntityState.Added:
                    entityWithTimestamps.Added = DateTime.UtcNow;
                    /*Log.Information($"Stamped for insert: {e.Entry.Entity}");*/
                    break;
            }
    }
}