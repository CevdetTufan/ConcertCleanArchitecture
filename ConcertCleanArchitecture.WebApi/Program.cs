using ConcertCleanArchitecture.Application;
using ConcertCleanArchitecture.Application.Dtos;
using ConcertCleanArchitecture.Application.Interfaces;
using ConcertCleanArchitecture.Infrastructure;
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