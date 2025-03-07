namespace ConcertCleanArchitecture.Application.Dtos.Auth;
public record LoginQueryDto(
	string UserNameOrEmail,
	string Password
);

