using Microsoft.Extensions.DependencyInjection;
using POSM.Fx.Utilities.Environments;
using POSM.Fx.Utilities.Interfaces;

namespace POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig
{
    public static class EnvironmentConfig
    {
        public static void ConfigServices(IServiceCollection services)
        {
            services.AddSingleton<IEnvironmentManager, EnvironmentManager>();
        }
    }
}
