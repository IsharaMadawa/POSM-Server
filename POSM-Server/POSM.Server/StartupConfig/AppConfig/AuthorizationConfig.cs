
namespace POSM.APIs.GraphQLServer.StartupConfig.AppConfig;
public static class AuthorizationConfig
{
    public static void ConfigApp(IApplicationBuilder app)
    {
        app.UseAuthorization();
    }
}
