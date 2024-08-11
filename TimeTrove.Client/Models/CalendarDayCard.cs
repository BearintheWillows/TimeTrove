namespace TimeTrove.Client.Models;

public class CalendarDayCard
{
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public bool IsPastDate { get; set; }
    public bool IsToday { get; set; }

    public CalendarDayCard(DateTime date)
    {
        Date = date;
        IsPastDate = Date.Date < DateTime.Today;
        IsToday = Date.Date == DateTime.Today;
    }
}