using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class RejectedApplicationsConfig : IEntityTypeConfiguration<RejectedApplications>
	{
		public void Configure(EntityTypeBuilder<RejectedApplications> builder)
		{
			builder.ToTable("RejectedApplications", "Business");
			builder.ToView("RejectedApplications", "Business");

			builder.Property(b => b.Description).IsRequired().HasMaxLength(400);
			builder.Property(b => b.OwnerId).IsRequired();
			builder.Property(b => b.ExpiredDate).IsRequired();
		}
	}
}
