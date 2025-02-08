using ConcertCleanArchitecture.Domain.Entities;
using ConcertCleanArchitecture.Domain.Interfaces;
using ConcertCleanArchitecture.Infrastructure.Persistence;

namespace ConcertCleanArchitecture.Infrastructure.Repositories;

internal class ConcertRepository(AppDbContext context) : Repository<Concert>(context), IConcertRepository
{
	private readonly AppDbContext _context = context;
	public async Task AddConcertRangeAsync(List<Concert> concertList)
	{
		await _context.Concerts.AddRangeAsync(concertList);
	}
}
