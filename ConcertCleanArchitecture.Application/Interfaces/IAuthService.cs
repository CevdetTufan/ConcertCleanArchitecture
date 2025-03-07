using ConcertCleanArchitecture.Application.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace ConcertCleanArchitecture.Application.Interfaces;
public interface IAuthService
{
	Task<string?> AuthenticateAsync(string email, string password);
	Task<IdentityResult> RegisterAsync(RegisterQueryDto model);
	Task<IdentityResult> ChangePassword(ChangePasswordDto model);
	Task<string> ForgotPassword(ForgotPasswordDto model);
	Task<IdentityResult> ResetPassword(ResetPasswordDto model);
}
