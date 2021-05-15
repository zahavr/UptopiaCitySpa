using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class VacancyConfig : IEntityTypeConfiguration<Vacancy>
	{
		public void Configure(EntityTypeBuilder<Vacancy> builder)
		{
			builder.ToTable("Vacancies", "Business");
			builder.ToView("Vacancies", "Business");

			builder.Property(b => b.Title).IsRequired().HasMaxLength(50);
			builder.Property(b => b.Description).IsRequired().HasMaxLength(1000);
			builder.Property(b => b.Salary).IsRequired().HasColumnType("decimal(18,2)");


			builder
				.HasOne(b => b.Business)
				.WithMany(b => b.Vacancies);

			builder
				.HasMany(b => b.VacancyApplications)
				.WithOne(b => b.Vacancy);
		}
	}
}
