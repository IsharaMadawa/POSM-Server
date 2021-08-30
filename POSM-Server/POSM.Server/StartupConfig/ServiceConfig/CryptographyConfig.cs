
using POSM.Fx.Cryptography;
using POSM.Fx.Cryptography.Interfaces;

namespace POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig;
public static class CryptographyConfig
{
    public static void ConfigServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPOSMHasher, POSMHasher>();
    }
}
