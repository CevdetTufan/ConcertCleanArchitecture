using ConcertCleanArchitecture.Application.Dtos;

namespace ConcertCleanArchitecture.Application.Interfaces;
public interface IConcertService
{
	Task<ConcertAddDto> AddConcertAsync(ConcertAddDto concertAddDto);
}
