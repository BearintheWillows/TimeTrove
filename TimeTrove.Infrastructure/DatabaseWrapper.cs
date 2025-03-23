using Npgsql;
using TimeTrove.Infrastructure.Interfaces;

namespace TimeTrove.Infrastructure;

public class DatabaseWrapper : IDatabaseWrapper
{
    private readonly NpgsqlConnection _connection;
    
    public DatabaseWrapper(string connection)
    {
        _connection = new (connection);
        _connection.Open();
        
    }
    
    public NpgsqlConnection GetConnection()
    {
        if (_connection.State == System.Data.ConnectionState.Closed)
        {
            _connection.Open();
        }
        return _connection;
    }
    
}