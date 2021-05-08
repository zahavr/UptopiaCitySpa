using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
		private readonly UserManager<User> _userManager;
		private readonly SymmetricSecurityKey _key;

		public TokenService(
			IConfiguration configuration,
			UserManager<User> userManager)
		{
			_configuration = configuration;
			_userManager = userManager;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
		}

		public async Task<string>  CreateToken(User user)
		{

			List<Claim> claims = new List<Claim>();

			IList<string> userRoles = await _userManager.GetRolesAsync(user);

			foreach (string userRole in userRoles)
			{
				claims.Add(new Claim(ClaimTypes.Role, userRole));
			}

			claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
			claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));

			SigningCredentials creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds,
				Issuer = _configuration["Token: Issuer"]
			};

			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
