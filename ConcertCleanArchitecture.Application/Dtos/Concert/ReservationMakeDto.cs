namespace ConcertCleanArchitecture.Application.Dtos.Concert;
public class ReservationMakeDto
{
	public Guid ConcertId { get; set; }
	public Guid SeatId { get; set; }
}
