using Microsoft.EntityFrameworkCore;
using TimeTrove.Client.Models;
using TimeTrove.Data;
using TimeTrove.Data.Models;

namespace TimeTrove.Services;

public interface IBudgetService
{
    Task<BudgetDTO> GetBudgetAsync(int budgetId);
    Task<BudgetDTO> CreateBudgetAsync(BudgetDTO budgetDto);
}

public class BudgetService : IBudgetService
{
    private readonly ApplicationDbContext _dbContext;

    public BudgetService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<BudgetDTO?> GetBudgetAsync(int budgetId)
    {
        var budget = await _dbContext.Budgets.AsNoTracking().FirstOrDefaultAsync(b => b.Id == budgetId);
        
        return budget != null ? Budget.ToDto(budget) : null;
        
    }

    public async Task<BudgetDTO> CreateBudgetAsync(BudgetDTO budgetDto)
    {
       
            ArgumentNullException.ThrowIfNull(budgetDto);

            // Transforming DTO to Entity
            Budget budget = Budget.FromDto(budgetDto);

            // Adding budget to dbContext
            await _dbContext.Budgets.AddAsync(budget);

            // Saving changes
            await _dbContext.SaveChangesAsync();

            return Budget.ToDto(budget);
        }
}