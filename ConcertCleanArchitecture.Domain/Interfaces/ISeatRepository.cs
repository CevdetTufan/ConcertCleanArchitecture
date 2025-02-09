using ConcertCleanArchitecture.Domain.Entities;

namespace ConcertCleanArchitecture.Domain.Interfaces;
public interface ISeatRepository : IRepository<Seat>
{
	Task<List<Seat>> GetSeatsByConcertIdAsync(Guid concertId);
}
