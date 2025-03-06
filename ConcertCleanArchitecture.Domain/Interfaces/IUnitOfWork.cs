namespace ConcertCleanArchitecture.Domain.Interfaces;
public interface IUnitOfWork
{
	IRolePermissionRepository RolePermissionRepository { get; }

	IConcertRepository ConcertRepository { get; }
	ISeatRepository SeatRepository { get; }
	Task<int> CompleteAsync();
}
