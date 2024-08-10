using Microsoft.EntityFrameworkCore;
using TimeTrove.Client.Models;
using TimeTrove.Data;
using TimeTrove.Data.Models;

namespace TimeTrove.Services;

public interface IBudgetService
{
    Task<BudgetDTO> GetBudgetAsync(int budgetId, bool includeItems);
    Task<object> CreateBudgetAsync(BudgetDTO budgetDto);
}

public class BudgetService : IBudgetService
{
    private readonly ApplicationDbContext _dbContext;

    public BudgetService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<BudgetDTO> GetBudgetAsync(int budgetId, bool includeItems)
    {
        // Querying the Budgets DbSet for a Budget with the specified ID, and including related BudgetItems based on the includeItems boolean.
        var budget = includeItems
            ? await _dbContext.Budgets.Include(b => b.Items).FirstOrDefaultAsync(b => b.Id == budgetId)
            : await _dbContext.Budgets.FirstOrDefaultAsync(b => b.Id == budgetId);

        return budget != null ? Budget.ToDto(budget) : null;
    }

    public Task<object> CreateBudgetAsync(BudgetDTO budgetDto)
    {
        throw NotImplementedException();
        
        /*TODO - Finish off*/
    }
}