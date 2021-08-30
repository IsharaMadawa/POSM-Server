
using Microsoft.IdentityModel.Protocols;
using POSM.Fx.Utilities.Interfaces;

namespace POSM.APIs.GraphQLServer;
public class OperationBase
{
	protected readonly IHttpContextAccessor httpContextAccessor;
	protected readonly IConfigurationManager configurationManager;
	protected readonly IEnvironmentManager environmentManager;

	public OperationBase(IHttpContextAccessor httpContextAccessor = null, IConfigurationManager configurationManager = null, IEnvironmentManager environmentManager = null)
	{
		this.httpContextAccessor = httpContextAccessor;
		this.configurationManager = configurationManager;
		this.environmentManager = environmentManager;
	}
}
