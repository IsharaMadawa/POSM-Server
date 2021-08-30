using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace POSM.APIs.GraphQLServer.StartupConfig.AppConfig
{
	public static class EndpointConfig
	{
		public static void ConfigApp(IApplicationBuilder app)
		{
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("POSM Server working!");
				});
				endpoints.MapGraphQL();
			});

			app.UseGraphQLVoyager(new VoyagerOptions()
			{
				GraphQLEndPoint = "/graphql"
			}, "/graphql-voyager");
		}
	}
}
