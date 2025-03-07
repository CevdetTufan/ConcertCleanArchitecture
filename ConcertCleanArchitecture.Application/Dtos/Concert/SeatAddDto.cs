namespace ConcertCleanArchitecture.Application.Dtos.Concert;

public class SeatAddDto
{
	public string Row { get; set; } = default!;
	public int Number { get; set; }
	public decimal Price { get; set; }
}
