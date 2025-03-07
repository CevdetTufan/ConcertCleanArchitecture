using Microsoft.AspNetCore.Identity;

namespace ConcertCleanArchitecture.Domain.Entities;
public class RolePermission
{
	public Guid RoleId { get; set; }
	public IdentityRole<Guid> Role { get; set; } = default!;

	public Guid PermissionId { get; set; }
	public Permission Permission { get; set; } = default!;
}
