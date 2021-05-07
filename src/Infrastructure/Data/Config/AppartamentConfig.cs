using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data.Config
{
	public class AppartamentConfig : IEntityTypeConfiguration<Appartament>
	{
		public void Configure(EntityTypeBuilder<Appartament> builder)
		{
			builder.ToTable("Appartaments", "HousingSystem");
			builder.ToView("Appartaments", "HousingSystem");

			builder.Property(ap => ap.Floor).IsRequired();
			builder.Property(ap => ap.CountRooms).IsRequired();
			builder.Property(ap => ap.Description).HasMaxLength(400).IsRequired();
			builder.Property(ap => ap.Title).HasMaxLength(40).IsRequired();

			builder
				.Property(ap => ap.Cost)
				.HasColumnType("decimal(18,2)")
				.IsRequired();

			builder
				.HasOne(ap => ap.Building)
				.WithMany(b => b.Appartaments);

			builder.Property(ap => ap.TypeAppartament)
				.HasConversion(
					tp => tp.ToString(),
					tp => (TypeAppartament)Enum.Parse(typeof(TypeAppartament), tp)
				);
		}
	}
}
