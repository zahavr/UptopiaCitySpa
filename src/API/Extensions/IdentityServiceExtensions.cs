using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
	public static class IdentityServiceExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
		{
			IdentityBuilder builder = services.AddIdentityCore<User>();

			builder = new IdentityBuilder(builder.UserType, builder.Services);
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
			});

			return services;
		}
	}
}
