using ConcertCleanArchitecture.Application.Dtos.Concert;
using FluentValidation;

namespace ConcertCleanArchitecture.Application.Validators;

internal class ConcertAddDtoValidator : AbstractValidator<ConcertAddDto>
{
	internal ConcertAddDtoValidator()
	{
		RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
		RuleFor(x => x.Date).NotEmpty();
		RuleFor(x => x.Venue).NotEmpty().MaximumLength(50);
		RuleForEach(x => x.Seats).SetValidator(new SeatAddDtoValidator());
		RuleFor(x => x.Seats).Must(x => x.Count > 3);
	}
}
