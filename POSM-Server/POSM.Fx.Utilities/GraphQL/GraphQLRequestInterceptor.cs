using System.Security.Claims;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;

namespace POSM.Fx.Utilities.GraphQL
{
    public class GraphQLRequestInterceptor : DefaultHttpRequestInterceptor
    {
        //IsharaK[30/08/2021]: Intercept the graphQL request creation with custom logic.
        public override async ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder, CancellationToken cancellationToken)
        {
            await ValueTask.FromResult(0);

            requestBuilder.TrySetServices(context.RequestServices);
            requestBuilder.TryAddProperty(nameof(HttpContext), context);
            requestBuilder.TryAddProperty(nameof(ClaimsPrincipal), context.User);
            requestBuilder.TryAddProperty(nameof(CancellationToken), context.RequestAborted);
        }
    }
}
