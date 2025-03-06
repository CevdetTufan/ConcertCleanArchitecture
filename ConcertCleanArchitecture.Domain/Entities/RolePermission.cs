using Microsoft.AspNetCore.Identity;

namespace ConcertCleanArchitecture.Domain.Entities;
public class RolePermission
{
	public string RoleId { get; set; } = default!;
	public IdentityRole Role { get; set; } = default!;

	public int PermissionId { get; set; }
	public Permission Permission { get; set; } = default!;
}
