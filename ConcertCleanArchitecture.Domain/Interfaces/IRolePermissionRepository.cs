namespace ConcertCleanArchitecture.Domain.Interfaces;
public interface IRolePermissionRepository
{
	Task<List<string>> GetPermissionsByRoleIdAsync(Guid roleId);
}
