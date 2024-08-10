using TimeTrove.Client.Models;

namespace TimeTrove.Data.Models;

public class Budget : AuditableEntity
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<BudgetItem> Items { get; set; }
    
    public int CategoryID { get; set; }
    public Category Category { get; set; }

    public static BudgetDTO ToDto(Budget budget)
    {
        ArgumentNullException.ThrowIfNull(budget);
        return new BudgetDTO
        {
            Id = budget.Id,
            Name = budget.Name,
            StartDate = budget.StartDate,
            EndDate = budget.EndDate,
            Items = budget.Items.Select(item => BudgetItem.ToDto(item)).ToList()
        };
    }

    public static Budget FromDto(BudgetDTO budgetDto)
    {
        ArgumentNullException.ThrowIfNull(budgetDto);

        return new Budget
        {
            Id = budgetDto.Id ?? null,
            Name = budgetDto.Name,
            StartDate = budgetDto.StartDate,
            EndDate = budgetDto.EndDate ?? null,
            Items = budgetDto.Items.Select(item => BudgetItem.FromDto(item)).ToList()
        };
    }
}

