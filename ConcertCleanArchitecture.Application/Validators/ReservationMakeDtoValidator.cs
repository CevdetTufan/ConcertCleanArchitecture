using ConcertCleanArchitecture.Application.Dtos;
using FluentValidation;

namespace ConcertCleanArchitecture.Application.Validators;

internal class ReservationMakeDtoValidator : AbstractValidator<ReservationMakeDto>
{
	public ReservationMakeDtoValidator()
	{
		RuleFor(x => x.ConcertId).NotEmpty().WithMessage("Boş olmamalı");
		RuleFor(x => x.SeatId).NotEmpty();
	}
}
