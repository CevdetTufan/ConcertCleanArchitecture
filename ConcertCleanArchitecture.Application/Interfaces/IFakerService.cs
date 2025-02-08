namespace ConcertCleanArchitecture.Application.Interfaces;

public interface IFakerService
{
	Task<int> SeedConcertDataAsync();
}
