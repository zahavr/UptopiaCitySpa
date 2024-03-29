﻿using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
	public class AppIdentityDbContext : IdentityDbContext<User, Role, string>
	{
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<User>()
				.Property(x => x.Money)
				.HasColumnType("decimal(18,2)");
		}
	}
}
