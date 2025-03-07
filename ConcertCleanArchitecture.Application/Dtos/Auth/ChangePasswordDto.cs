namespace ConcertCleanArchitecture.Application.Dtos.Auth;
public record ChangePasswordDto(Guid UserId, string CurrentPassword, string NewPassword);

