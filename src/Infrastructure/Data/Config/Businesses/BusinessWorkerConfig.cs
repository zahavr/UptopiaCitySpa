using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class BusinessWorkerConfig : IEntityTypeConfiguration<BusinessWorker>
	{
		public void Configure(EntityTypeBuilder<BusinessWorker> builder)
		{
			builder.ToTable("BusinessWorkers", "Business");
			builder.ToView("BusinessWorkers", "Business");

			builder.Property(bw => bw.PositionAtWork).IsRequired().HasMaxLength(40);
			builder.Property(bw => bw.StartWork).IsRequired();
			builder.Property(bw => bw.WorkerId).IsRequired();

			builder.Property(bw => bw.Salary)
				.HasColumnType("decimal(18,2)")
				.IsRequired();

			builder
				.HasOne(bw => bw.Business)
				.WithMany(b => b.BusinessWorkers);
		}
	}
}
