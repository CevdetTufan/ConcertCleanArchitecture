using ConcertCleanArchitecture.Application.Dtos.Concert;
using ConcertCleanArchitecture.Application.Interfaces;
using ConcertCleanArchitecture.Application.Validators;
using ConcertCleanArchitecture.Domain.Entities;
using ConcertCleanArchitecture.Domain.Interfaces;
using Mapster;
using System.ComponentModel.DataAnnotations;

namespace ConcertCleanArchitecture.Application.Services;

internal class ConcertService(IUnitOfWork uow) : IConcertService
{
	private readonly IUnitOfWork _uow = uow;

	public async Task<ConcertAddDto> AddConcertAsync(ConcertAddDto concertAddDto)
	{
		var validation = new ConcertAddDtoValidator().Validate(concertAddDto);
		if (!validation.IsValid)
		{
			throw new ValidationException(string.Join(", ", validation.Errors));
		}

		var concert = concertAddDto.Adapt<Concert>();

		var addedConcert = _uow.ConcertRepository.Add(concert);
		await _uow.CompleteAsync();

		return addedConcert.Adapt<ConcertAddDto>();
	}

	public async Task MakeReservation(ReservationMakeDto reservation)
	{
		var validation = new ReservationMakeDtoValidator().Validate(reservation);

		if (!validation.IsValid)
		{
			throw new ValidationException(string.Join(", ", validation.Errors));
		}

		var concert = await _uow.ConcertRepository.GetByIdAsync(reservation.ConcertId);
		if (concert is null)
		{
			throw new KeyNotFoundException("Concert not found");
		}

		if (concert.Date.ToUniversalTime() < DateTime.UtcNow)
		{
			throw new ValidationException("Concert is already finished");
		}

		var seat = await _uow.SeatRepository.GetByIdAsync(reservation.SeatId);
		if (seat is null)
		{
			throw new KeyNotFoundException("Seat not found");
		}
		if (seat.IsReserved)
		{
			throw new ValidationException("Seat is already reserved");
		}
		seat.IsReserved = true;
		_uow.SeatRepository.Update(seat);
		await _uow.CompleteAsync();
	}

	public IQueryable<ConcertQueryDto> GetConcerts()
	{
		var concerts = _uow.ConcertRepository.GetConcerts();
		return concerts.ProjectToType<ConcertQueryDto>();
	}

	public async Task<List<SeatDto>> GetSeatsByConcertId(Guid concertId)
	{
		var seats = await _uow.SeatRepository.GetSeatsByConcertIdAsync(concertId);

		return seats.Adapt<List<SeatDto>>();
	}
}
