
using POSM.Fx.Utilities.Interfaces;
using POSM.Fx.Utilities.Configurations;

namespace POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig;
public static class ConfigurationConfig
{
    public static void ConfigServices(IServiceCollection services)
    {
        services.AddSingleton<IConfigurationManager, ConfigurationsManager>();
    }
}
