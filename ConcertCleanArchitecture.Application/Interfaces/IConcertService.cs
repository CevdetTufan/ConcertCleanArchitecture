using ConcertCleanArchitecture.Application.Dtos;

namespace ConcertCleanArchitecture.Application.Interfaces;
public interface IConcertService
{
	Task<ConcertAddDto> AddConcertAsync(ConcertAddDto concertAddDto);
	Task MakeReservation(ReservationMakeDto reservation);
	IQueryable<ConcertQueryDto> GetConcerts();
	Task<List<SeatDto>> GetSeatsByConcertId(Guid concertId);
}
