using Npgsql;

namespace TimeTrove.Infrastructure.Interfaces;

public interface IDatabaseWrapper
{
    public NpgsqlConnection GetConnection();
}