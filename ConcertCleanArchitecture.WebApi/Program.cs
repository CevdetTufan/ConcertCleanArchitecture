using Scalar.AspNetCore;
using ConcertCleanArchitecture.Infrastructure;
using ConcertCleanArchitecture.Application;
using ConcertCleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.OData;


var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddControllers()
	.AddOData(opt => opt.EnableQueryFeatures());

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services
	.AddInfrastructure(builder.Configuration)
	.AddApplication();

var app = builder.Build();

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
