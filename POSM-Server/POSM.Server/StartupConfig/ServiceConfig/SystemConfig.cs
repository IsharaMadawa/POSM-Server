
namespace POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig;
public class SystemConfig
{
    public static void ConfigServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
    }
}
