using System.Security.Claims;
using POSM.Core.Data.Db.Models;

namespace POSM.FX.Security.Interfaces
{
	public interface ITokenValidator
	{
		string GetJWTAuthKey(User user, List<UserRole> roles);

		string GenerateRefreshToken();

		ClaimsPrincipal GetClaimsFromExpiredToken(string accessToken);
	}
}
