
namespace POSM.APIs.GraphQLServer.StartupConfig.AppConfig;
public static class AuthenticationConfig
{
    public static void ConfigApp(IApplicationBuilder app)
    {
        app.UseAuthentication();
    }
}
