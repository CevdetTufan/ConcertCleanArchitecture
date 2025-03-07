using ConcertCleanArchitecture.Application.Dtos.Auth;
using ConcertCleanArchitecture.Application.Dtos.Result;
using Microsoft.AspNetCore.Identity;

namespace ConcertCleanArchitecture.Application.Interfaces;
public interface IAuthService
{
	Task<ResultDto> AuthenticateAsync(LoginQueryDto model);
	Task<IdentityResult> RegisterAsync(RegisterQueryDto model);
	Task<IdentityResult> ChangePassword(ChangePasswordDto model);
	Task<string> ForgotPassword(ForgotPasswordDto model);
	Task<IdentityResult> ResetPassword(ResetPasswordDto model);
}
