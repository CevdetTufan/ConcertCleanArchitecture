using ConcertCleanArchitecture.Application.Dtos.Auth;
using ConcertCleanArchitecture.Application.Interfaces;
using ConcertCleanArchitecture.Domain.Entities;
using ConcertCleanArchitecture.Domain.Interfaces;
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
	private readonly IUnitOfWork _uow;
	private readonly IConfiguration _configuration;

	public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration, IUnitOfWork uow)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_uow = uow;
		_configuration = configuration;
	}

	public async Task<string?> AuthenticateAsync(string email, string password)
	{
		var user = await _userManager.FindByEmailAsync(email);
		if (user is null || !await _userManager.CheckPasswordAsync(user, password))
		{
			return null;
		}
		return await GenerateJwtToken(user);
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
		var idendityResult = await _userManager.CreateAsync(new ApplicationUser
		{
			Email = model.Email,
			UserName = model.Email
		}, model.Password);

		return idendityResult;
	}
}
