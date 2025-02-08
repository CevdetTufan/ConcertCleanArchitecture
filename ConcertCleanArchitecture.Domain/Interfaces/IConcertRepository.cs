using ConcertCleanArchitecture.Domain.Entities;

namespace ConcertCleanArchitecture.Domain.Interfaces;
public interface IConcertRepository : IRepository<Concert>
{
	Task AddConcertRangeAsync(List<Concert> concertList);
	IQueryable<Concert> GetConcerts();
}
