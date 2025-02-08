using ConcertCleanArchitecture.Domain.Abstraction;

namespace ConcertCleanArchitecture.Domain.Entities;
public class Seat : BaseEntity
{
	public Guid ConcertId { get; set; }
	public string Row { get; set; } = default!;
	public int Number { get; set; }
	public decimal Price { get; set; }
	public bool IsReserved { get; set; }
	public DateTime? ReservationDate { get; set; }

	public virtual Concert Concert { get; set; } = default!;
}
