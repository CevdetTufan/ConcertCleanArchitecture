using ConcertCleanArchitecture.Domain.Entities;
using ConcertCleanArchitecture.Domain.Interfaces;
using ConcertCleanArchitecture.Infrastructure.Persistence;

namespace ConcertCleanArchitecture.Infrastructure.Repositories;

internal class SeatRepository(AppDbContext context) : Repository<Seat>(context), ISeatRepository
{
}
