namespace TimeTrove.Data.Models;

public class Budget : AuditableEntity
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<BudgetItem> Items { get; set; }
    
    public int CategoryID { get; set; }
    public Category Category { get; set; }
}