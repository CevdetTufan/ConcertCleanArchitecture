

using ConcertCleanArchitecture.Domain.Abstraction;

namespace ConcertCleanArchitecture.Domain.Entities;
public class ApplicationUser : BaseUserEntity
{
	public string FullName { get; set; } = default!;
}
