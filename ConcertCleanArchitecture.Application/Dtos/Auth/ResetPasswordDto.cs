namespace ConcertCleanArchitecture.Application.Dtos.Auth;
public record ResetPasswordDto(string UserNameOrEmail, string Token, string NewPassword);

