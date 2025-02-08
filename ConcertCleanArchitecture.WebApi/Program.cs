using Scalar.AspNetCore;
using ConcertCleanArchitecture.Infrastructure;
using ConcertCleanArchitecture.Application;
using ConcertCleanArchitecture.Application.Interfaces;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services
	.AddInfrastructure(builder.Configuration)
	.AddApplication();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/seed-concerr", async (IFakerService fakerService) =>
{
	int createdItems = await fakerService.SeedConcertDataAsync();
	return Results.Created("/seed", $"{createdItems} adet data oluşturuldu.");
});

app.MapOpenApi();
app.MapScalarApiReference();

app.Run();
