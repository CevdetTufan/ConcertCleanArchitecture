using ConcertCleanArchitecture.Application.Dtos;
using FluentValidation;

namespace ConcertCleanArchitecture.Application.Validators;
public class SeatAddDtoValidator : AbstractValidator<SeatAddDto>
{
	public SeatAddDtoValidator()
	{
		RuleFor(x => x.Row).NotEmpty().MaximumLength(10);
		RuleFor(x => x.Number).Must(x => x > 0).NotEmpty();
		RuleFor(x => x.Price).NotEmpty();
	}
}
