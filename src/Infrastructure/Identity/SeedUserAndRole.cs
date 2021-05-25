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
						},
						new Role {
							Id = Guid.NewGuid().ToString(),
							Name = "CityManager",
							Description = "Can managment city"
						},
						new Role {
							Id = Guid.NewGuid().ToString(),
							Name = "Sheriff",
							Description = "Main person of police departament. Can manage police departament."
						},
						new Role {
							Id = Guid.NewGuid().ToString(),
							Name = "Officer",
							Description = "Regular worker of police departament."
						},
						new Role {
							Id = Guid.NewGuid().ToString(),
							Name = "BusinessOwner",
							Description = "Role for user whom has business."
						}
					};

				foreach (Role role in roles)
				{
					await userRole.CreateAsync(role);
				}
			}
			

			if (!userManager.Users.Any())
			{
				User admin = new User
				{
					UserName = "admin",
					Email = "admin@admin.com",
					BirthDate = DateTime.Now,
					Money = 5000,
					FirstName = "Alexey",
					LastName = "Zakharov",
					PhoneNumber = "+7777777777"
				};

				await userManager.CreateAsync(admin, "Pa$$w0rd");
				await userManager.AddToRolesAsync(admin, new List<string> { "Admin", "Citizen" });

				User manager = new User
				{
					UserName = "manager",
					Email = "manager@admin.com",
					BirthDate = DateTime.Now,
					Money = 5000,
					FirstName = "Alexey",
					LastName = "Zakharov",
					PhoneNumber = "+6666666666"
				};

				await userManager.CreateAsync(manager, "Pa$$w0rd");
				await userManager.AddToRolesAsync(manager, new List<string> { "CityManager", "Citizen" });

				User sheriff = new User
				{
					UserName = "sheriff",
					Email = "sheriff@admin.com",
					BirthDate = DateTime.Now,
					Money = 5000,
					FirstName = "Alexey",
					LastName = "Zakharov",
					PhoneNumber = "+7777777777"
				};

				await userManager.CreateAsync(sheriff, "Pa$$w0rd");
				await userManager.AddToRolesAsync(sheriff, new List<string>{"Sheriff", "Citizen"});
			}
		}
	}
}
