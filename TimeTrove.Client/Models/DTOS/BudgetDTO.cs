using TimeTrove.Client.Models.Shared;

namespace TimeTrove.Client.Models;

public class BudgetDTO
{
    public int? Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public Frequency Frequency { get; set; }
    
    public CategoryDTO Category { get; set; }
    public string? Note { get; set; }
}