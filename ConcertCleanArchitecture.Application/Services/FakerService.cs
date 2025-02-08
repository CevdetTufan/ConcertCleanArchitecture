using Bogus;
using ConcertCleanArchitecture.Application.Interfaces;
using ConcertCleanArchitecture.Domain.Entities;
using ConcertCleanArchitecture.Domain.Interfaces;

namespace ConcertCleanArchitecture.Application.Services;

internal class FakerService(IUnitOfWork uow) : IFakerService
{
	private readonly IUnitOfWork _uow = uow;

	private static List<Concert> GenerateConcerts(int count = 1000)
	{
		// Concert Faker
		var concertFaker = new Faker<Concert>()
			.RuleFor(c => c.Id, f => Guid.NewGuid())
			.RuleFor(c => c.Name, f =>
			{
				var name = f.Name.FindName();
				return name[..Math.Min(50, name.Length)];
			})
			.RuleFor(c => c.Date, f => f.Date.Future())
			.RuleFor(c => c.Venue, f => f.Address.City())
			.RuleFor(c => c.Venue, f =>
			{
				var city = f.Address.City();
				return city[..Math.Min(50, city.Length)];
			})
			.RuleFor(c => c.CreatedAt, f => DateTime.UtcNow)
			.RuleFor(c => c.Seats, f => []);

		// Seat Faker
		var seatFaker = new Faker<Seat>()
			.RuleFor(s => s.Id, f => Guid.NewGuid())
			.RuleFor(s => s.Row, f => f.Random.AlphaNumeric(5))
			.RuleFor(s => s.Number, f => f.Random.Int(1, 50))
			.RuleFor(s => s.Price, f => f.Finance.Amount(20, 2000))
			.RuleFor(s => s.IsReserved, f => f.Random.Bool())
			.RuleFor(s => s.ReservationDate, (f, s) => s.IsReserved ? f.Date.Past() : (DateTime?)null)
			.RuleFor(s => s.CreatedAt, f => DateTime.UtcNow);

		var concerts = new List<Concert>();

		for (int i = 0; i < count; i++)
		{
			var concert = concertFaker.Generate();

			concert.Seats = seatFaker.GenerateBetween(5, 20);

			foreach (var seat in concert.Seats)
			{
				seat.ConcertId = concert.Id;
			}

			concerts.Add(concert);
		}

		return concerts;
	}

	public async Task<int> SeedConcertDataAsync()
	{
		var concerts = GenerateConcerts(1000);
		await _uow.ConcertRepository.AddConcertRangeAsync(concerts);
		return await _uow.CompleteAsync();
	}
}
