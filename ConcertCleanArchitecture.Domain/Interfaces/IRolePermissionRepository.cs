namespace ConcertCleanArchitecture.Domain.Interfaces;
public interface IRolePermissionRepository
{
	Task<List<string>> GetPermissionsByRoleIdAsync(string roleId);
}
