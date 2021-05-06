using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class BuildingConfig : IEntityTypeConfiguration<Building>
	{
		public void Configure(EntityTypeBuilder<Building> builder)
		{
			builder.ToView("Buildings", "HousingSystem");

			builder.Property(b => b.CountFloor).IsRequired();
			builder.Property(b => b.CountApartments).IsRequired();
			builder.Property(b => b.Street).IsRequired();

			builder
				.HasMany(b => b.Appartaments)
				.WithOne(ap => ap.Building);
		}
	}
}
