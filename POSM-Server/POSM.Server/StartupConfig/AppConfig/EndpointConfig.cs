using GraphQL.Server.Ui.Voyager;

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
						 //.RequireAuthorization(PolicyConstants.POLICY_INTERNAL_ADMIN); //IsharaK[31/08/2021]: Secure the entire endpoint; any request to this endpoint must carry a valid token. switch off the security control and always allow to access our API, for only focusing on functionality builds.
			});

			app.UseGraphQLVoyager(new VoyagerOptions()
			{
				GraphQLEndPoint = "/graphql"
			}, "/graphql-voyager");
		}
	}
}
