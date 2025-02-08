namespace ConcertCleanArchitecture.Domain.Interfaces;
public interface IUnitOfWork
{
	IConcertRepository ConcertRepository { get; }
	ISeatRepository SeatRepository { get; }
	Task<int> CompleteAsync();
}
