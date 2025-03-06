namespace ConcertCleanArchitecture.Application.Interfaces;
public interface IAuthService
{
	Task<string?> AuthenticateAsync(string email, string password);
}
