using Dapper;

namespace TimeTrove.Infrastructure;

public class BaseRepository() : IBaseRepository
{
    
    private readonly DatabaseWrapper _db;
    
    public BaseRepository(DatabaseWrapper databaseWrapper) : this()
    {
        _db = databaseWrapper;
    }

    public async Task ExecuteAsync(string sql, object? param = null)
    {
        await using var connection = _db.GetConnection();
        await connection.ExecuteAsync(sql, param);
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object? param = null)
    {
        await using var connection = _db.GetConnection();
        return await connection.QuerySingleAsync<T>(sql, param);
    }
    
}