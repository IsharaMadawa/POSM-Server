
using POSM.Fx.Utilities.Interfaces;

namespace POSM.APIs.GraphQLServer.GraphQL.Mutations;
public class MutationBase : OperationBase
{
	public MutationBase(IHttpContextAccessor httpContextAccessor = null, IConfigurationManager configurationManager = null, IEnvironmentManager environmentManager = null) : base(httpContextAccessor, configurationManager, environmentManager)
	{
	}
}
