using ConcertCleanArchitecture.Application.Dtos;
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
			throw new ValidationException(string.Join(",", validation.Errors));
		}

		var concert = concertAddDto.Adapt<Concert>();

		var addedConcert = _uow.ConcertRepository.Add(concert);
		await _uow.CompleteAsync();

		return addedConcert.Adapt<ConcertAddDto>();
	}
}
