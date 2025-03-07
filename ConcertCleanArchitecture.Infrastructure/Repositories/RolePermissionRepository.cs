using ConcertCleanArchitecture.Domain.Entities;
using ConcertCleanArchitecture.Domain.Interfaces;
using ConcertCleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConcertCleanArchitecture.Infrastructure.Repositories;
internal class RolePermissionRepository(AppDbContext context) : Repository<RolePermission>(context), IRolePermissionRepository
{
	private readonly AppDbContext _context = context;

	public async Task<List<string>> GetPermissionsByRoleIdAsync(Guid roleId)
	{
		var rolePermissions = await _context.RolePermissions
				.Where(rp => rp.RoleId == roleId)
				.Select(rp => rp.Permission.Name)
				.ToListAsync();

		return rolePermissions;
	}
}
