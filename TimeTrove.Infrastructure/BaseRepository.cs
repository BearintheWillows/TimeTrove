using Dapper;
using TimeTrove.Infrastructure.Interfaces;

namespace TimeTrove.Infrastructure;

public abstract class BaseRepository() : IBaseRepository
{
    
    private readonly DatabaseWrapper _db;
    protected abstract string MainQueryTableName { get; }
    
    public BaseRepository(DatabaseWrapper databaseWrapper) : this()
    {
        _db = databaseWrapper;
    }

    public async Task<T> GetByIdAsync<T>(int id)
    {
        await using var connection = _db.GetConnection();
        var result = await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {MainQueryTableName} WHERE Id = @Id", new { Id = id });
        return result;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync<T>()
    {
        await using var connection = _db.GetConnection();
        var result = await connection.QueryAsync<T>($"SELECT * FROM {MainQueryTableName}");
        return result;
    }
    
    public async Task<int> InsertAsync<T>(T entity)
    {
        await using var connection = _db.GetConnection();
        var result = await connection.ExecuteAsync($"INSERT INTO {MainQueryTableName} VALUES (@entity)", new { entity });
        return result;
    }
    
    public async Task<int> UpdateAsync<T>(T entity)
    {
        await using var connection = _db.GetConnection();
        var result = await connection.ExecuteAsync($"UPDATE {MainQueryTableName} SET @entity WHERE Id = @Id", new { entity });
        return result;
    }
    
    public async Task<int> DeleteAsync(int id)
    {
        await using var connection = _db.GetConnection();
        var result = await connection.ExecuteAsync($"DELETE FROM {MainQueryTableName} WHERE Id = @Id", new { Id = id });
        return result;
    }
    
    public async Task<int> UpsertAsync<T>(T entity, int id)
    {
        await using var connection = _db.GetConnection();
        var result = await connection.ExecuteAsync($@"
        IF EXISTS (SELECT 1 FROM {MainQueryTableName} WHERE Id = @Id)
        BEGIN
            UPDATE {MainQueryTableName} SET @entity WHERE Id = @Id
        END
        ELSE
        BEGIN
            INSERT INTO {MainQueryTableName} VALUES (@entity)
        END", new { Id = id, entity });
        return result;
    }
    
    public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, object parameters = null)
    {
        await using var connection = _db.GetConnection();
        var result = await connection.QueryAsync<T>(query, parameters);
        return result;
    }
    
    public async Task<int> ExecuteCommandAsync(string command, object parameters = null)
    {
        await using var connection = _db.GetConnection();
        var result = await connection.ExecuteAsync(command, parameters);
        return result;
    }
    
}