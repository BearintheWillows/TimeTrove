
using System.ComponentModel.DataAnnotations.Schema;
using TimeTrove.Client.Models;
using TimeTrove.Client.Models.Shared;

namespace TimeTrove.Data.Models;

public class Budget : AuditableEntity
{
    public int? Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<DateTime> ExcludedDates { get; set; }
    
    public int FrequencyID { get; set; }
    
    [ForeignKey("FrequencyID")]
    public Frequency Frequency { get; set; }
    
    public string Note { get; set; } = string.Empty;
    
    
     [ForeignKey("Category")]
     public int CategoryID { get; set; }
    
     public Category Category { get; set; }
     
    
    public string OwnerID { get; set; }
    
    [ForeignKey("OwnerID")]
    public ApplicationUser Owner { get; set; }

    public static BudgetDTO ToDto(Budget budget)
    {
        ArgumentNullException.ThrowIfNull(budget);
        
        return new BudgetDTO
        {
            Id = budget.Id, StartDate = budget.StartDate, EndDate = budget.EndDate, Note = budget.Note, Category = Category.ToDto(budget.Category), Frequency = budget.Frequency
        };
    }
    
    public static Budget FromDto(BudgetDTO budgetDTO)
    {
        ArgumentNullException.ThrowIfNull(budgetDTO);

        return new Budget
        {
            Id = budgetDTO.Id,
            StartDate = budgetDTO.StartDate,
            EndDate = budgetDTO.EndDate,
            Note = budgetDTO.Note,
            Category = Category.FromDto(budgetDTO.Category),
            Frequency = budgetDTO.Frequency
        };
    }
}

