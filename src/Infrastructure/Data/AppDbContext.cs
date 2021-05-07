using Core.Entities;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext([NotNull] DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Building> Buildings { get; set; }
		public DbSet<Appartament> Appartaments { get; set; }
		public DbSet<UserAppartament> UserAppartaments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AppartamentConfig());
			modelBuilder.ApplyConfiguration(new BuildingConfig());
			modelBuilder.ApplyConfiguration(new UserAppartamentConfig());
		}
	}
}
