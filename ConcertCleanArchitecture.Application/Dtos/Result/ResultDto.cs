namespace ConcertCleanArchitecture.Application.Dtos.Result;
public class ResultDto
{
	public virtual bool IsSuccess { get; set; }
	public string Message { get; set; } = default!;
}

public class ResultDataDto<T> : ResultDto
{
	public T Data { get; set; } = default!;
	override public bool IsSuccess => !EqualityComparer<T>.Default.Equals(Data, default);
}
