using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using POSM.Core.Data.Db.Models;
using POSM.FX.Security.Interfaces;

namespace POSM.FX.Security.OpenIDConnect
{
	public class TokenValidator : ITokenValidator
	{
		private readonly TokenSettings tokenSettings;

		public TokenValidator(IOptions<TokenSettings> tokenSettings)
		{
			this.tokenSettings = tokenSettings.Value;
		}

		public string GetJWTAuthKey(User user, List<UserRole> roles)
		{
			var securtityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key));

			var credentials = new SigningCredentials(securtityKey, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>();

			claims.Add(new Claim("Email", user.EmailAddress));
			claims.Add(new Claim("LastName", user.LastName));

			if ((roles?.Count ?? 0) > 0)
			{
				foreach (var role in roles)
				{
					claims.Add(new Claim(ClaimTypes.Role, role.Name));
				}
			}

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: tokenSettings.Issuer,
				audience: tokenSettings.Audience,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: credentials,
				claims: claims
			);

			return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
		}
	}
}
