using ConcertCleanArchitecture.Application.Dtos.Auth;
using ConcertCleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConcertCleanArchitecture.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
	private readonly IAuthService _authService = authService;

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

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterQueryDto model)
	{
		var result = await _authService.RegisterAsync(model);

		if(!result.Succeeded)
		{
			return BadRequest(result.Errors);
		}

		return Created();
	}
}
