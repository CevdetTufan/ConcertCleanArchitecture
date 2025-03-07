using ConcertCleanArchitecture.Application.Dtos.Auth;
using ConcertCleanArchitecture.Application.Dtos.Result;
using ConcertCleanArchitecture.Application.Interfaces;
using ConcertCleanArchitecture.Application.Validators;
using ConcertCleanArchitecture.Domain.Entities;
using ConcertCleanArchitecture.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConcertCleanArchitecture.Application.Services;
internal class AuthService : IAuthService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly IUnitOfWork _uow;
	private readonly IConfiguration _configuration;

	public AuthService(
		UserManager<ApplicationUser> userManager,
		RoleManager<IdentityRole<Guid>> roleManager,
		SignInManager<ApplicationUser> signInManager,
		IConfiguration configuration,
		IUnitOfWork uow)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_signInManager = signInManager;
		_uow = uow;
		_configuration = configuration;
	}

	public async Task<ResultDto> AuthenticateAsync(LoginQueryDto model)
	{
		var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);

		if (user is null)
		{
			user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
		}

		if (user is null)
		{
			return new ResultDto { Message = "User was not found." };
		}

		var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

		if (result.IsLockedOut)
		{
			TimeSpan? timeSpan = user.LockoutEnd?.Subtract(DateTime.UtcNow);
			if (timeSpan.HasValue && timeSpan.Value.TotalMinutes > 0)
			{
				return new ResultDto
				{
					Message = $"Your account has been locked out for {timeSpan.Value.TotalSeconds:N0} seconds due to multiple unsuccessful login attempts. Please try again later."
				};
			}
		}

		if (result.IsNotAllowed)
		{
			return new ResultDto
			{
				Message = "Login is not allowed. Please check your email and complete the verification process."
			};
		}

		if (!result.Succeeded)
		{
			return new ResultDto { Message = "Invalid credentials" };
		}

		return new ResultDataDto<dynamic>
		{
			Data = new { Token = "Generated Token" },
			Message = "Login successful",
		};
	}

	private async Task<string> GenerateJwtToken(ApplicationUser user)
	{
		var userRoles = await _userManager.GetRolesAsync(user);
		var claims = new List<Claim>
							{
								new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
								new(JwtRegisteredClaimNames.Email, user.Email!),
								new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
							};

		foreach (var role in userRoles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));

			var roleEntity = await _roleManager.FindByNameAsync(role);

			if (roleEntity is not null)
			{
				var rolePermissions = await _uow.RolePermissionRepository.GetPermissionsByRoleIdAsync(roleEntity.Id);

				foreach (var permission in rolePermissions)
				{
					claims.Add(new Claim("Permission", permission));
				}
			}
		}

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var expires = DateTime.UtcNow.AddHours(2);

		var token = new JwtSecurityToken(
			_configuration["Jwt:Issuer"],
			_configuration["Jwt:Audience"],
			claims,
			expires: expires,
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}


	public async Task<int> SeedAsync()
	{
		// Define Roles
		var roles = new List<string> { "Admin", "User", "Manager" };

		foreach (var role in roles)
		{
			if (!await _roleManager.RoleExistsAsync(role))
			{
				await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
			}
		}

		// Create an Admin User
		var adminEmail = "admin@example.com";
		var adminUser = await _userManager.FindByEmailAsync(adminEmail);
		if (adminUser == null)
		{
			var newAdmin = new ApplicationUser
			{
				UserName = adminEmail,
				Email = adminEmail,
				EmailConfirmed = true
			};

			var result = await _userManager.CreateAsync(newAdmin, "P@ssword1!");
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(newAdmin, "Admin");
			}
		}

		return await _uow.CompleteAsync();
	}

	public async Task<IdentityResult> RegisterAsync(RegisterQueryDto model)
	{
		var appUserModel = model.Adapt<ApplicationUser>();

		var idendityResult = await _userManager.CreateAsync(appUserModel, model.Password);

		return idendityResult;
	}

	public async Task<IdentityResult> ChangePassword(ChangePasswordDto model)
	{
		var user = await _userManager.FindByIdAsync(model.UserId.ToString());
		if (user is null)
		{
			throw new KeyNotFoundException("User not found");
		}

		var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

		return result;
	}

	public async Task<string> ForgotPassword(ForgotPasswordDto model)
	{
		var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);

		if (user is null)
		{
			user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
		}

		if (user is null)
		{
			throw new KeyNotFoundException("User not found");
		}

		var token = await _userManager.GeneratePasswordResetTokenAsync(user);

		return token;
	}

	public async Task<IdentityResult> ResetPassword(ResetPasswordDto model)
	{
		var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
		if (user is null)
		{
			user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
		}

		if (user is null)
		{
			throw new KeyNotFoundException("User not found");
		}

		var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
		return result;
	}
}
