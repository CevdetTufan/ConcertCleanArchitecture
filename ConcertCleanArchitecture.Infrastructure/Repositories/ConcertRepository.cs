using ConcertCleanArchitecture.Domain.Entities;
using ConcertCleanArchitecture.Domain.Interfaces;
using ConcertCleanArchitecture.Infrastructure.Persistence;

namespace ConcertCleanArchitecture.Infrastructure.Repositories;

internal class ConcertRepository(AppDbContext context) : Repository<Concert>(context), IConcertRepository
{
}
