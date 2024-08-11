

using TimeTrove.Data.Models;

namespace TimeTrove.Client.Models;

public class CalendarDay
{
    public int? Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public List<Transaction> Transactions { get; set; }
}