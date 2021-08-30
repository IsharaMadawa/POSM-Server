
using POSM.FX.Diagnostics.Logging;

namespace POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig;
public static class LoggingServiceConfig
{
    public static void ConfigServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IPOSMLogger<>), typeof(POSMLogger<>));
    }
}
