using POSM.Core.Business.Operations.Auth;
using POSM.Core.Business.Operations.Interfaces;
using POSM.Core.Business.Operations.Items;

namespace POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig
{
	public static class BusinessConfig
	{
		public static void ConfigServices(IServiceCollection services)
		{
			services.AddScoped<IAuthOperator, AuthOperator>();
			services.AddScoped<IItemOperator, ItemOperator>();
		}
	}
}
