using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class VacancyApplicationsConfig : IEntityTypeConfiguration<VacancyApplications>
	{
		public void Configure(EntityTypeBuilder<VacancyApplications> builder)
		{
			builder.ToTable("VacancyApplications", "Business");
			builder.ToView("VacancyApplications", "Business");

			builder.Property(va => va.ApplicantId).IsRequired();
			builder.Property(va => va.VacancyStatus).IsRequired();

			builder
				.HasOne(va => va.Vacancy)
				.WithMany(va => va.VacancyApplications)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
