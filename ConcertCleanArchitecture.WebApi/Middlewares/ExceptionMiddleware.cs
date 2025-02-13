using System.ComponentModel.DataAnnotations;

namespace ConcertCleanArchitecture.WebApi.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
	private readonly RequestDelegate _next = next;
	private readonly ILogger<ExceptionMiddleware> _logger = logger;

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (KeyNotFoundException ex)
		{
			await HandleSpecificExceptionAsync(context, ex, StatusCodes.Status404NotFound);
		}
		catch (ValidationException ex)
		{
			await HandleSpecificExceptionAsync(context, ex, StatusCodes.Status400BadRequest);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);
			await HandleExceptionAsync(context);
		}
	}

	private async Task HandleSpecificExceptionAsync(HttpContext context, Exception exception, int statusCode)
	{
		_logger.LogError(exception, exception.Message);
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = statusCode;
		var errorResponse = new
		{
			exception.Message
		};
		await context.Response.WriteAsJsonAsync(errorResponse);
	}

	private static async Task HandleExceptionAsync(HttpContext context)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		var errorResponse = new
		{
			Message = "An unexpected error occurred."
		};
		await context.Response.WriteAsJsonAsync(errorResponse);
	}
}
