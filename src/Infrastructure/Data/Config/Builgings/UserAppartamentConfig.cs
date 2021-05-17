using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class UserAppartamentConfig : IEntityTypeConfiguration<UserAppartament>
	{
		public void Configure(EntityTypeBuilder<UserAppartament> builder)
		{
			builder.ToTable("UserAppartament", "HousingSystem");
			builder.ToView("UserAppartament", "HousingSystem");

			builder.Property(up => up.UserId).IsRequired();

			builder.HasKey(ap => new { ap.AppartamentId });

			builder
				.HasOne(up => up.Appartament)
				.WithMany(a => a.UserAppartaments)
				.HasForeignKey(up => up.AppartamentId);
		}
	}
}
