namespace TimeTrove.Infrastructure;

public interface IBaseRepository
{

    Task<T> QuerySingleAsync<T>(string sql, object? param = null);
    Task ExecuteAsync(string sql, object? param = null);
}