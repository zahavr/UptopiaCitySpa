using Core.Entities;
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
	}
}
