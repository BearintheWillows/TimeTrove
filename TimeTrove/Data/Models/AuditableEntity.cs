using TimeTrove.Data.Interfaces;

namespace TimeTrove.Data.Models;

public abstract class AuditableEntity : IHasTimestamps
{
    public DateTime Added { get; set; }
    public DateTime? Modified { get; set; }
    public DateTime? Deleted { get; set; }
}