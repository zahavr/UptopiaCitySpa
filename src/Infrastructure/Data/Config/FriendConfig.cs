using Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class FriendConfig : IEntityTypeConfiguration<Friend>
	{
		public void Configure(EntityTypeBuilder<Friend> builder)
		{
			builder.ToTable("Users", "Friends");
			builder.ToView("Users", "Friends");

			builder.Property(f => f.UserId).IsRequired();
			builder.Property(f => f.FriendId).IsRequired();
			builder.Property(f => f.UserFirstName).IsRequired();
			builder.Property(f => f.UserLastName).IsRequired();
			builder.Property(f => f.BirthDateUser).IsRequired();
			builder.Property(f => f.FriendFirstName).IsRequired();
			builder.Property(f => f.FriendLastName).IsRequired();
			builder.Property(f => f.FriendBirthDate).IsRequired();
			builder.Property(f => f.FriendEmail).IsRequired();
			builder.Property(f => f.FriendStatus).IsRequired();
		}
	}
}
