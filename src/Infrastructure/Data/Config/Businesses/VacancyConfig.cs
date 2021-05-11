using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class VacancyConfig : IEntityTypeConfiguration<Vacancy>
	{
		public void Configure(EntityTypeBuilder<Vacancy> builder)
		{
			builder.ToTable("Vacansies", "Business");
			builder.ToView("Vacansies", "Business");

			builder.Property(b => b.Title).IsRequired().HasMaxLength(50);
			builder.Property(b => b.Description).IsRequired().HasMaxLength(1000);
			builder.Property(b => b.Salary).IsRequired().HasColumnType("decimal(18,2)");


			builder
				.HasOne(b => b.Businesses)
				.WithMany(b => b.Vacancies);
		}
	}
}
