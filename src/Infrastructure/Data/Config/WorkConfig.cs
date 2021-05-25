using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class WorkConfig : IEntityTypeConfiguration<Shift>
	{
		public void Configure(EntityTypeBuilder<Shift> builder)
		{
			builder.ToTable("Shifts", "Work");
			builder.ToView("Shifts", "Work");

			builder.Property(s => s.UserId).IsRequired();
			builder.Property(s => s.StartShift).IsRequired();
			builder.Property(s => s.EarnedMoney).HasColumnType("decimal(18,2)");
		}
	}
}
