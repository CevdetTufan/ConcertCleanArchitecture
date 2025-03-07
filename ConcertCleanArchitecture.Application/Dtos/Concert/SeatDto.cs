namespace ConcertCleanArchitecture.Application.Dtos.Concert;

public class SeatDto
{
	public Guid Id { get; set; }
	public string Row { get; set; } = default!;
	public int Number { get; set; }
	public decimal Price { get; set; }
	public bool IsReserved { get; set; }
	public DateTime? ReservationDate { get; set; }
}
