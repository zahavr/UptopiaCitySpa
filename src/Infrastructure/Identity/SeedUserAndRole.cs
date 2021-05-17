using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
	public static class SeedUserAndRole
	{
		public static async Task SeedUserAndRoleAsync(UserManager<User> userManager, RoleManager<Role> userRole)
		{
			if (!userManager.Users.Any())
			{
				User user = new User
				{
					UserName = "admin",
					Email = "admin@admin.com",
					BirthDate = DateTime.Now,
					Money = 10000000,
				};

				await userManager.CreateAsync(user, "Pa$$w0rd");

				if (!userRole.Roles.Any())
				{
					List<Role> roles = new List<Role> {
						new Role {
							Id = Guid.NewGuid().ToString(),
							Name = "Admin",
							Description = "Super mod"
						},
						new Role {
							Id = Guid.NewGuid().ToString(),
							Name = "Citizen",
							Description = "Default role for all users"
						}
					};

					foreach (Role role in roles)
					{
						await userRole.CreateAsync(role);
					}

					await userManager.AddToRoleAsync(user, "Admin");
				}
			}
		}
	}
}
