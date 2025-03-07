using ConcertCleanArchitecture.Application;
using ConcertCleanArchitecture.Application.Dtos.Concert;
using ConcertCleanArchitecture.Application.Interfaces;
using ConcertCleanArchitecture.Infrastructure;
using ConcertCleanArchitecture.WebApi.Logging;
using ConcertCleanArchitecture.WebApi.Middlewares;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

var odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<ConcertQueryDto>("GetConcerts");

builder.Services
	.AddControllers()
	 .AddOData(opt =>
	 {
		 opt.EnableQueryFeatures()
			.Count()
		    .AddRouteComponents("api/[controller]", odataBuilder.GetEdmModel());
	 });

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services
	.AddInfrastructure(builder.Configuration)
	.AddApplication();


builder.Services.AddSingleton<ILoggerProvider>(provider =>
	new FileLoggerProvider(provider.GetRequiredService<IConfiguration>()));
builder.Services.AddLogging();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapPost("/seed-concert", async (IFakerService fakerService) =>
{
	int createdItems = await fakerService.SeedConcertDataAsync();
	return Results.Created("/seed-concert", $"{createdItems} adet data oluþturuldu.");
});

app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();


await app.RunAsync();