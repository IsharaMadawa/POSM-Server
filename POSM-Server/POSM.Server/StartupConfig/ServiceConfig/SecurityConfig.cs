using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POSM.FX.Security;

namespace POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig
{
	public static class SecurityConfig
	{
		public static void ConfigServices(IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));
		}
	}
}
