using ConcertCleanArchitecture.Domain.Entities;
using ConcertCleanArchitecture.Domain.Interfaces;
using ConcertCleanArchitecture.Infrastructure.Persistence;
using ConcertCleanArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConcertCleanArchitecture.Infrastructure;
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		string connectionString = configuration.GetConnectionString("DefaultConnection")!;
		services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

		services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
		{
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireUppercase = false;
			options.Password.RequireLowercase = false;
			options.Password.RequireDigit = false;
			options.Password.RequiredLength = 6;
		})
		.AddEntityFrameworkStores<AppDbContext>()
		.AddDefaultTokenProviders();

		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		services.AddScoped<IConcertRepository, ConcertRepository>();
		services.AddScoped<ISeatRepository, SeatRepository>();
		services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

		return services;
	}
}
