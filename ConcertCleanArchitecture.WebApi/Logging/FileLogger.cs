using System.Collections.Concurrent;

namespace ConcertCleanArchitecture.WebApi.Logging;

public class FileLogger : ILogger, IDisposable
{
	private readonly string filePath;
	private readonly string categoryName;
	private readonly LogLevel logLevel;

	private static readonly SemaphoreSlim semaphore = new(1, 1);
	private readonly ConcurrentQueue<string> logQueue = new();
	private readonly Task logProcessorTask;
	private bool isProcessing = true;

	private bool _disposed = false;

	public FileLogger(string filePath, string categoryName, LogLevel logLevel)
	{
		this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		this.categoryName = categoryName ?? throw new ArgumentNullException(nameof(categoryName));
		this.logLevel = logLevel;

		logProcessorTask = Task.Run(ProcessLogQueue);
	}

	public IDisposable? BeginScope<TState>(TState state) where TState : notnull
	{
		return null;
	}

	public bool IsEnabled(LogLevel logLevel)
	{
		return logLevel >= this.logLevel;
	}

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
	{
		if (!IsEnabled(logLevel)) return;

		string dailyFilePath = Path.Combine(filePath, $"Log_{DateTime.Now:yyyy-MM-dd}.log");

		string logRecord = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {categoryName}: {formatter(state, exception)}{Environment.NewLine}";

		if (exception != null)
		{
			logRecord += $"Exception: {exception.Message}{Environment.NewLine}";
		}

		logQueue.Enqueue($"{dailyFilePath}|{logRecord}");
	}

	private async Task ProcessLogQueue()
	{
		while (isProcessing || !logQueue.IsEmpty)
		{
			if (logQueue.TryDequeue(out string? logEntry))
			{
				var parts = logEntry.Split('|', 2);
				var dailyFilePath = parts[0];
				var logRecord = parts[1];

				await semaphore.WaitAsync();
				try
				{
					Directory.CreateDirectory(Path.GetDirectoryName(dailyFilePath) ?? string.Empty);
					await File.AppendAllTextAsync(dailyFilePath, logRecord);
				}
				finally
				{
					semaphore.Release();
				}
			}
			else
			{
				await Task.Delay(100);
			}
		}
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				isProcessing = false;
				logProcessorTask.Wait();
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
