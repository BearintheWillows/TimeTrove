using Npgsql;

namespace TimeTrove.Infrastructure;

public interface IDatabaseWrapper
{
    public NpgsqlConnection GetConnection();
}