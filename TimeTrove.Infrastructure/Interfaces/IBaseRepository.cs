namespace TimeTrove.Infrastructure.Interfaces;

public interface IBaseRepository
{

    Task<T> GetByIdAsync<T>(int id);
    Task<IEnumerable<T>> GetAllAsync<T>();
}