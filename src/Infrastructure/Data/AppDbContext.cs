using Core.Entities;
using Core.Entities.User;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

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
		public DbSet<Friend> Friends { get; set; }
		public DbSet<Business> Businesses { get; set; }
		public DbSet<BusinessWorker> BusinessWorkers { get; set; }
		public DbSet<RejectedApplications> RejectedApplications { get; set; }
		public DbSet<VacancyApplications> VacancyApplications { get; set; }
		public DbSet<Vacancy> Vacancies { get; set; }
		public DbSet<Violation> Violations { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			/*			modelBuilder.ApplyConfiguration(new AppartamentConfig());
						modelBuilder.ApplyConfiguration(new BuildingConfig());
						modelBuilder.ApplyConfiguration(new UserAppartamentConfig());
						modelBuilder.ApplyConfiguration(new FriendConfig());
						modelBuilder.ApplyConfiguration(new BusinessWorkerConfig());
						modelBuilder.ApplyConfiguration(new BusinessConfig());
						modelBuilder.ApplyConfiguration(new RejectedApplicationsConfig());
						modelBuilder.ApplyConfiguration(new VacancyConfig());
						modelBuilder.ApplyConfiguration(new VacancyApplicationsConfig());
						modelBuilder.ApplyConfiguration(new ViolationConfig());*/
		}
	}
}
