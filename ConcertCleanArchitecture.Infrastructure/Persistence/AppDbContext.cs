﻿using ConcertCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConcertCleanArchitecture.Infrastructure.Persistence;
internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<Concert> Concerts { get; set; } 
	public DbSet<Seat> Seats { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Concert>(entity =>
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

		modelBuilder.Entity<Seat>(entity =>
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
