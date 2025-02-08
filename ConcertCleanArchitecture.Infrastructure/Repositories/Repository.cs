using ConcertCleanArchitecture.Domain.Interfaces;
using ConcertCleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConcertCleanArchitecture.Infrastructure.Repositories;

internal class Repository<T>(AppDbContext context) : IRepository<T> where T : class
{
	private readonly DbSet<T> _dbSet = context.Set<T>();

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task<T?> GetByIdAsync(Guid id)
	{
		return await _dbSet.FindAsync(id);
	}

	public T Add(T entity)
	{
		var entry = _dbSet.Add(entity);
		return entry.Entity;
	}

	public T Update(T entity)
	{
		var entry = _dbSet.Update(entity);
		return entry.Entity;
	}

	public async Task<T> DeleteAsync(Guid id)
	{
		var entity = await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException("Entity not found");
		var entry = _dbSet.Remove(entity);
		return entry.Entity;
	}
}
