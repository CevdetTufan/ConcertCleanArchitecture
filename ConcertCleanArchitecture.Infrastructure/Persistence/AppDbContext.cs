using ConcertCleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConcertCleanArchitecture.Infrastructure.Persistence;
internal class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
	//AppUser
	public DbSet<Permission> Permissions { get; set; }
	public DbSet<RolePermission> RolePermissions { get; set; }

	//Concert
	public DbSet<Concert> Concerts { get; set; }
	public DbSet<Seat> Seats { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		// Define Many-to-Many Relationship between Roles and Permissions
		builder.Entity<RolePermission>()
			.HasKey(rp => new { rp.RoleId, rp.PermissionId });

		builder.Entity<RolePermission>()
			.HasOne(rp => rp.Role)
			.WithMany()
			.HasForeignKey(rp => rp.RoleId);

		builder.Entity<RolePermission>()
			.HasOne(rp => rp.Permission)
			.WithMany()
			.HasForeignKey(rp => rp.PermissionId);

		builder.Entity<Concert>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50);

			entity.HasIndex(e => e.Name)
				.HasDatabaseName("IX_Concert_Name");

			entity.Property(e => e.Date)
				.IsRequired();

			entity.Property(e => e.Venue)
				.IsRequired()
				.HasMaxLength(50);

			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("GETDATE()");

			entity.Property(e => e.IsDeleted)
				.HasDefaultValue(false);

			entity.HasQueryFilter(e => !e.IsDeleted);

			entity.HasMany(e => e.Seats);
		});

		builder.Entity<Seat>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.Property(e => e.ConcertId)
				.IsRequired();

			entity.Property(e => e.Row)
				.IsRequired()
				.HasMaxLength(5);

			entity.Property(e => e.Number)
				.IsRequired();

			entity.Property(e => e.Price)
				.IsRequired()
				.HasColumnType("money");

			entity.Property(e => e.IsReserved)
				.HasDefaultValue(false);

			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("GETDATE()");

			entity.Property(e => e.IsDeleted)
				.HasDefaultValue(false);

			entity.HasOne(e => e.Concert)
				.WithMany(e => e.Seats)
				.HasForeignKey(e => e.ConcertId);

			entity.HasQueryFilter(e => !e.IsDeleted);
		});
	}
}
