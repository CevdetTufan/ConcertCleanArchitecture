using ConcertCleanArchitecture.Domain.Abstraction;

namespace ConcertCleanArchitecture.Domain.Entities;
public class Concert : BaseEntity
{
	public string Name { get; set; } = default!;
	public DateTime Date { get; set; }
	public string Venue { get; set; } = default!;

	public virtual ICollection<Seat> Seats { get; set; } = [];
}
