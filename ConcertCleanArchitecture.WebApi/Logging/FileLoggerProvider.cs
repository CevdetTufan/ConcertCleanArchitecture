namespace ConcertCleanArchitecture.WebApi.Logging;

public class FileLoggerProvider(IConfiguration configuration) : ILoggerProvider
{
	private readonly IConfiguration configuration = configuration;

	private  FileLogger? fileLogger;
	private bool _disposed = false;

	public ILogger CreateLogger(string categoryName)
	{
		string filePath = configuration["Logging:File:Path"]!;
		LogLevel logLevel = Enum.Parse<LogLevel>(configuration["Logging:LogLevel:Default"]!);

		fileLogger = new FileLogger(filePath, categoryName, logLevel);
		return fileLogger;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				fileLogger?.Dispose();
			}

			_disposed = true;
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
