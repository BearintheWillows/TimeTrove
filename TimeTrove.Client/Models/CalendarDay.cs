using System.Runtime.InteropServices.JavaScript;

namespace TimeTrove.Client.Models;

public class CalendarDay
{
    public DateTime Date { get; set; }
    public string Title { get; set; }
    
    public int DayNum { get; set; }
    public bool IsPastDate { get; set; }
    public bool IsToday { get; set; }

    public CalendarDay(DateTime date)
    {
        Date = date;
        DayNum = date.Day; // gets the day of the month
        IsPastDate = Date.Date < DateTime.Today;
        IsToday = Date.Date == DateTime.Today;
    }
}