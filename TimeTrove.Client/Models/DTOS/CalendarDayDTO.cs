namespace TimeTrove.Client.Models;

public class CalendarDayDTO
{
    public int? Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public List<TransactionDTO> Transactions { get; set; }
}