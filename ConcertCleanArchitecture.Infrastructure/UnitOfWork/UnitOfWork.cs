using ConcertCleanArchitecture.Domain.Interfaces;
using ConcertCleanArchitecture.Infrastructure.Persistence;
using ConcertCleanArchitecture.Infrastructure.Repositories;

namespace ConcertCleanArchitecture.Infrastructure.UnitOfWork;
internal class UnitOfWork(AppDbContext context) : IUnitOfWork, IDisposable
{
	private bool _disposed = false;
	private readonly AppDbContext _context = context;

	public IConcertRepository ConcertRepository => new ConcertRepository(_context);

	public ISeatRepository SeatRepository => new SeatRepository(_context);

	public async Task<int> CompleteAsync()
	{
		return await _context.SaveChangesAsync();
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed && disposing)
		{
			_context.Dispose();
		}
		_disposed = true;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
