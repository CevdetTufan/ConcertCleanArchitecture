using Scalar.AspNetCore;
using ConcertCleanArchitecture.Infrastructure;
using ConcertCleanArchitecture.Application;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services
	.AddInfrastructure(builder.Configuration)
	.AddApplication();

var app = builder.Build();

app.UseHttpsRedirection();



app.MapOpenApi();
app.MapScalarApiReference();

app.Run();
