namespace TimeTrove.Client.Models;

public class BudgetDTO
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<BudgetItemDTO> Items { get; set; }
}