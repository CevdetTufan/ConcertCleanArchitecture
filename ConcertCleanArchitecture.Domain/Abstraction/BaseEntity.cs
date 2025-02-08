namespace ConcertCleanArchitecture.Domain.Abstraction;
public abstract class BaseEntity
{
	protected BaseEntity()
	{
		Id = Guid.NewGuid();
	}
	public Guid Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
}
