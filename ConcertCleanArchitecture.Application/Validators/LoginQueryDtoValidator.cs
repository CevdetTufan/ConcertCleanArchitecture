using ConcertCleanArchitecture.Application.Dtos.Auth;
using FluentValidation;

namespace ConcertCleanArchitecture.Application.Validators;
internal class LoginQueryDtoValidator: AbstractValidator<LoginQueryDto>
{
	public LoginQueryDtoValidator()
	{
		RuleFor(x => x.UserNameOrEmail).NotEmpty().WithMessage("Username is required");
		RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
	}
}
