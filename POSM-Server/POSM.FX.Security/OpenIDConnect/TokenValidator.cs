using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
			SymmetricSecurityKey securtityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key));

			SigningCredentials credentials = new SigningCredentials(securtityKey, SecurityAlgorithms.HmacSha256);

			List<Claim> claims = new List<Claim>();

			claims.Add(new Claim("Email", user.EmailAddress));
			claims.Add(new Claim("LastName", user.LastName));

			if ((roles?.Count ?? 0) > 0)
			{
				foreach (UserRole role in roles)
				{
					claims.Add(new Claim(ClaimTypes.Role, role.Name));
				}
			}

			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
				issuer: tokenSettings.Issuer,
				audience: tokenSettings.Audience,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: credentials,
				claims: claims
			);

			return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
		}

		public string GenerateRefreshToken()
		{
			byte[] randomNumber = new byte[32];
			using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
			{
				generator.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}

		public ClaimsPrincipal GetClaimsFromExpiredToken(string accessToken)
		{
			TokenValidationParameters tokenValidationParameter = new TokenValidationParameters
			{
				ValidIssuer = tokenSettings.Issuer,
				ValidateIssuer = true,
				ValidAudience = tokenSettings.Audience,
				ValidateAudience = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
				ValidateLifetime = false // IsharaK[04/09/2021] : Ignore expiration, because here our target to decrypt the expired token.
			};

			JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
			ClaimsPrincipal principal = jwtHandler.ValidateToken(accessToken, tokenValidationParameter, out SecurityToken securityToken);

			JwtSecurityToken jwtScurityToken = securityToken as JwtSecurityToken;
			if (jwtScurityToken == null)
			{
				return null;
			}

			return principal;
		}
	}
}
