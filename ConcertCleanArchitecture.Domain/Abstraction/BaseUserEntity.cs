using Microsoft.AspNetCore.Identity;

namespace ConcertCleanArchitecture.Domain.Abstraction;
public class BaseUserEntity: IdentityUser
{
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
}
