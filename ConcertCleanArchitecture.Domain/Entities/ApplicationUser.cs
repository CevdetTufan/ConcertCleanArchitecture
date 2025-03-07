using Microsoft.AspNetCore.Identity;

namespace ConcertCleanArchitecture.Domain.Entities;
public class ApplicationUser : IdentityUser<Guid>
{
	public string FullName { get; set; } = default!;
}
