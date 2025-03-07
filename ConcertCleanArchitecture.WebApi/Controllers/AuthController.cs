using ConcertCleanArchitecture.Application.Dtos.Auth;
using ConcertCleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConcertCleanArchitecture.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
	private readonly IAuthService _authService = authService;

	[HttpPost("Login")]
	public async Task<IActionResult> Login([FromBody] LoginQueryDto model)
	{
		var token = await _authService.AuthenticateAsync(model);
		if (!token.IsSuccess)
		{
			return Unauthorized(token);
		}

		return Ok(token);
	}

	[HttpPost("Register")]
	public async Task<IActionResult> Register([FromBody] RegisterQueryDto model)
	{
		var result = await _authService.RegisterAsync(model);

		if (!result.Succeeded)
		{
			return BadRequest(result.Errors);
		}

		return Created();
	}

	[HttpPost("ChangePassword")]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
	{
		var result = await _authService.ChangePassword(model);
		if (!result.Succeeded)
		{
			return BadRequest(result.Errors);

		}
		return Ok();
	}

	[HttpPost("ForgotPassword")]
	public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
	{
		string token = await _authService.ForgotPassword(model);
		if (string.IsNullOrWhiteSpace(token))
		{
			return NotFound();
		}
		return Ok(new { Token = token });
	}

	[HttpPost("ResetPassword")]
	public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
	{
		var result = await _authService.ResetPassword(model);
		if (!result.Succeeded)
		{
			return BadRequest(result.Errors);
		}
		return Ok();
	}
}
