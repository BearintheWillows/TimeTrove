using TimeTrove.Client.Models.Enums.Enums;

namespace TimeTrove.Client.Models.Shared;

public class Frequency
{
    public int Id { get; set; }
    public int Count { get; set; }
    public TimeUnit Unit { get; set; }
}