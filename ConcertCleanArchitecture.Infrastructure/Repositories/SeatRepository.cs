using ConcertCleanArchitecture.Domain.Entities;
using ConcertCleanArchitecture.Domain.Interfaces;
using ConcertCleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConcertCleanArchitecture.Infrastructure.Repositories;

internal class SeatRepository(AppDbContext context) : Repository<Seat>(context), ISeatRepository
{
	private readonly AppDbContext _context = context;
	public async Task<List<Seat>> GetSeatsByConcertIdAsync(Guid concertId)
	{
		return await _context.Seats.Where(q => q.ConcertId == concertId).ToListAsync();
	}
}
