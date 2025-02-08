namespace ConcertCleanArchitecture.Domain.Interfaces;
public interface IRepository<T> where T : class
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<T?> GetByIdAsync(Guid id);
	T Add(T entity);
	T Update (T entity);
	Task<T> DeleteAsync(Guid id);
}
