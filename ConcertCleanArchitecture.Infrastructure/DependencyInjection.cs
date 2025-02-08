using ConcertCleanArchitecture.Domain.Interfaces;
using ConcertCleanArchitecture.Infrastructure.Persistence;
using ConcertCleanArchitecture.Infrastructure.Repositories;
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

		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		services.AddScoped<IConcertRepository, ConcertRepository>();
		services.AddScoped<ISeatRepository, SeatRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

		return services;
	}
}
