using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class BusinessConfig : IEntityTypeConfiguration<Business>
	{
		public void Configure(EntityTypeBuilder<Business> builder)
		{
			builder.ToTable("Business", "Business");
			builder.ToView("Business", "Business");

			builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
			builder.Property(b => b.Description).IsRequired().HasMaxLength(1000);
			builder.Property(b => b.MaxCountOfWorker).IsRequired();
			builder.Property(b => b.OwnerId).IsRequired();
			builder.Property(b => b.Address).IsRequired();

			builder
				.HasMany(b => b.BusinessWorkers)
				.WithOne(b => b.Business);

			builder
				.HasMany(b => b.Vacancies)
				.WithOne(v => v.Businesses);
		}
	}
}
