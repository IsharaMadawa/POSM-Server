using Microsoft.AspNetCore.Builder;

namespace POSM.APIs.GraphQLServer.StartupConfig.AppConfig
{
    public static class RoutingConfig
    {
        public static void ConfigApp(IApplicationBuilder app)
        {
            app.UseRouting();
        }
    }
}
