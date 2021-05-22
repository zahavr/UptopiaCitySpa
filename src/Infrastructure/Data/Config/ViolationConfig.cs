using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class ViolationConfig : IEntityTypeConfiguration<Violation>
	{
		public void Configure(EntityTypeBuilder<Violation> builder)
		{
			builder.ToTable("Violations", "Police");
			builder.ToView("Violations", "Police");

			builder.Property(v => v.PolicemanId).IsRequired();
			builder.Property(v => v.CitizenId).IsRequired();
			builder.Property(v => v.SetDate).IsRequired();
			builder.Property(v => v.Penalty)
				.HasColumnType("decimal(18,2)")
				.IsRequired();
			builder.Property(v => v.Description)
				.HasMaxLength(1000)
				.IsRequired();
			builder.Property(v => v.DateExpired).IsRequired();
		}
	}
}
