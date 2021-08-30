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
		}
	}
}
