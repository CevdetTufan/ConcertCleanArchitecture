using ConcertCleanArchitecture.Application.Dtos;
using ConcertCleanArchitecture.Domain.Entities;

namespace ConcertCleanArchitecture.Application.Interfaces;
public interface IConcertService
{
	Task<ConcertAddDto> AddConcertAsync(ConcertAddDto concertAddDto);
	IQueryable<ConcertQueryDto> GetConcerts();
}
