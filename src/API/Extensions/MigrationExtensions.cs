using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Extensions
{
	public static class MigrationExtensions
	{
		public static async Task<IHost> ApplyMigrations(this IHost host)
		{
			using (IServiceScope scope = host.Services.CreateScope())
			{
				IServiceProvider services = scope.ServiceProvider;
				ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();

				try
				{
					AppDbContext appContext = services.GetRequiredService<AppDbContext>();
					await appContext.Database.MigrateAsync();

					UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();
					RoleManager<Role> roleManager = services.GetRequiredService<RoleManager<Role>>();
					AppIdentityDbContext identityContext = services.GetRequiredService<AppIdentityDbContext>();
					await identityContext.Database.MigrateAsync();

					await SeedUserAndRole.SeedUserAndRoleAsync(userManager, roleManager);
				}
				catch (Exception ex)
				{
					ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
					logger.LogError(ex, "An error occured during migration");
				}
			}

			return host;
		}
	}
}
