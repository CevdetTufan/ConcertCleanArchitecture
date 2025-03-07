namespace ConcertCleanArchitecture.Application.Dtos.Concert;
public class ConcertQueryDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public DateTime Date { get; set; }
	public string Venue { get; set; } = default!;
}
