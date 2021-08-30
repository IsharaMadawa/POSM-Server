using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POSM.APIs.GraphQLServer.GraphQL.Mutations.Items;
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
            graphServerBuilder.AddProjections()
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
                              .AddTypeExtension<ItemMutation>()
                              .AddTypeExtension<UserMutation>();
            #endregion
        }
    }
}
