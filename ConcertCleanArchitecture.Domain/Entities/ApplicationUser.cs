using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ConcertCleanArchitecture.Domain.Entities;
public class ApplicationUser : IdentityUser<Guid>
{
	[Required]
	[MaxLength(100), MinLength(3)]
	public string FullName { get; set; } = default!;
}
