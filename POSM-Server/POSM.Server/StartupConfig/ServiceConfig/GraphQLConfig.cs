using HotChocolate.Execution.Configuration;
using POSM.APIs.GraphQLServer.GraphQL.Mutations.Items;
using POSM.APIs.GraphQLServer.GraphQL.Mutations.Login;
using POSM.APIs.GraphQLServer.GraphQL.Mutations.Users;
using POSM.APIs.GraphQLServer.GraphQL.Queries.Items;
using POSM.APIs.GraphQLServer.GraphQL.Queries.Users;
using POSM.Fx.Utilities.GraphQL;
using POSM.FX.Diagnostics.ExceptionHandling;

namespace POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig
{
	public static class GraphQLConfig
	{
        public static void ConfigServices(IServiceCollection services)
        {
            services.AddErrorFilter<GraphQLErrorFilter>();

            IRequestExecutorBuilder externalGraphServerBuilder = services.AddGraphQLServer();
            ConfigAPIGraphQLServer(externalGraphServerBuilder);
        }

        private static void ConfigAPIGraphQLServer(IRequestExecutorBuilder graphServerBuilder)
        {
            graphServerBuilder.AddAuthorization() // IsharaK[31/08/2021] Need "AddAuthorization()" in "SecurityConfig" class as well  
                              .AddProjections()
                              .AddFiltering()
                              .AddSorting()
                              .AddHttpRequestInterceptor<GraphQLRequestInterceptor>();

            #region Query registration
            graphServerBuilder.AddQueryType(d => d.Name("Query"))
                              .AddTypeExtension<ItemQuery>()
                              .AddTypeExtension<UserQuery>();
            #endregion

            #region Mutation registration
            graphServerBuilder.AddMutationType(d => d.Name("Mutation"))
                              .AddTypeExtension<LoginMutataion>()
                              .AddTypeExtension<ItemMutation>()
                              .AddTypeExtension<UserMutation>();
            #endregion
        }
    }
}
