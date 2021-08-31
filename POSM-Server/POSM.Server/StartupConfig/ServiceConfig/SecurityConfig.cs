using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using POSM.Fx.Utilities.Constants;
using POSM.FX.Security.Interfaces;
using POSM.FX.Security.OpenIDConnect;

namespace POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig
{
	public static class SecurityConfig
	{
		public static void ConfigServices(IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));
			services.AddScoped<ITokenValidator, TokenValidator>();
			// IsharaK[31/08/2021] Need "AddAuthorization()" in "GraphQLConfig" class as well  
			services.AddAuthorization(options =>
			{
				// IsharaK[31/08/2021] : Policy-Based Roles Authorizatio - Currently using this approach. Below approches are added as example and use whenever needed
				options.AddPolicy(PolicyConstants.POLICY_INTERNAL_ADMIN, policy => { policy.RequireRole(new string[] { "admin", "cashier" }); });
				options.AddPolicy(PolicyConstants.POLICY_INTERNAL_CASHIER, policy => { policy.RequireRole(new string[] { "cashier" }); });

				// IsharaK[31/08/2021] : Policy-Based Claims Authorization - Aauthorize the user with his claims availability
				//options.AddPolicy("claim-policy-1", policy => {
				//	policy.RequireClaim("LastName");
				//});

				// IsharaK[31/08/2021] : Policy-Based Claims Authorization - authorize the user with his claim values
				//options.AddPolicy("claim-policy-2", policy => {
				//	policy.RequireClaim("LastName", new string[] { "Bommidi", "Test" });
				//});

			});

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				var tokenSettings = configuration
				.GetSection("TokenSettings").Get<TokenSettings>();
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = tokenSettings.Issuer,
					ValidateIssuer = true,
					ValidAudience = tokenSettings.Audience,
					ValidateAudience = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
					ValidateIssuerSigningKey = true
				};
			});
		}
	}
}
