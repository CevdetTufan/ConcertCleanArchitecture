using ConcertCleanArchitecture.Application.Interfaces;
using ConcertCleanArchitecture.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ConcertCleanArchitecture.Application;
public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		var assembly = typeof(DependencyInjection).Assembly;

		services.AddValidatorsFromAssembly(assembly);

		services.AddScoped<IConcertService, ConcertService>();
		services.AddScoped<IFakerService, FakerService>();

		return services;
	}
}
