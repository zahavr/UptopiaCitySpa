using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace API.Extensions
{
	public static class IdentityServiceExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
		{
			IdentityBuilder builder = services.AddIdentityCore<User>();
			builder = new IdentityBuilder(builder.UserType, builder.Services);
			builder.AddRoles<Role>();
			builder.AddRoleValidator<RoleValidator<Role>>();
			builder.AddRoleManager<RoleManager<Role>>();
			builder.AddEntityFrameworkStores<AppIdentityDbContext>();
			builder.AddSignInManager<SignInManager<User>>();


			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(opt =>
			{
				opt.TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
					ValidateIssuerSigningKey = true,
					ValidIssuer = configuration["Token:Issuer"],
					ValidateIssuer = false,
					ValidateAudience = false
				};

				opt.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						StringValues accessToken = context.Request.Query["access_token"];

						PathString path = context.HttpContext.Request.Path;
						if (!string.IsNullOrEmpty(accessToken) &&
								(path.StartsWithSegments("/message")))
						{
							context.Token = accessToken;
						}

						return Task.CompletedTask;
					}
				};
			});

			return services;
		}
	}
}
