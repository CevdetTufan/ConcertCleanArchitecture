using ConcertCleanArchitecture.Application.Dtos;
using ConcertCleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConcertCleanArchitecture.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginQueryDto model)
	{
		var token = await _authService.AuthenticateAsync(model.UserName, model.Password);
		if (string.IsNullOrWhiteSpace(token))
		{
			return Unauthorized("Invalid credentials");
		}

		return Ok(new { Token = token });
	}
}
