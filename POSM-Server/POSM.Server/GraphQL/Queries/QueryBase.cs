
using POSM.Fx.Cryptography.Interfaces;
using POSM.Fx.Utilities.Interfaces;

namespace POSM.APIs.GraphQLServer.GraphQL.Queries;
public class QueryBase : OperationBase
{
	public QueryBase(IHttpContextAccessor httpContextAccessor = null, IConfigurationManager configurationManager = null, IEnvironmentManager environmentManager = null, IPOSMHasher posmHasher = null) : base(httpContextAccessor, configurationManager, environmentManager, posmHasher)
	{
	}
}
