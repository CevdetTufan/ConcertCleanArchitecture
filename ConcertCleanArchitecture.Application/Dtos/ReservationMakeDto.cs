namespace ConcertCleanArchitecture.Application.Dtos;
public class ReservationMakeDto
{
	public Guid ConcertId { get; set; }
	public Guid SeatId { get; set; }
}
