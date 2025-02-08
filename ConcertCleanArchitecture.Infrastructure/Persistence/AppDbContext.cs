using ConcertCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConcertCleanArchitecture.Infrastructure.Persistence;
internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<Concert> Concerts { get; set; } 
	public DbSet<Seat> Seats { get; set; }
}
