namespace ConcertCleanArchitecture.Application.Dtos.Auth;
public record RegisterQueryDto(
	string UserName, 
	string Password, 
	string Email, 
	string FullName);

