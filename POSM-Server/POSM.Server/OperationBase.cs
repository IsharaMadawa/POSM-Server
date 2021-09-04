
using POSM.Fx.Cryptography.Interfaces;
using POSM.Fx.Utilities.Interfaces;

namespace POSM.APIs.GraphQLServer;
public class OperationBase
{
	protected readonly IHttpContextAccessor httpContextAccessor;
	protected readonly IConfigurationManager configurationManager;
	protected readonly IEnvironmentManager environmentManager;
	protected readonly IPOSMHasher posmHasher;

	public OperationBase(IHttpContextAccessor httpContextAccessor = null, IConfigurationManager configurationManager = null, IEnvironmentManager environmentManager = null, IPOSMHasher posmHasher = null)
	{
		this.httpContextAccessor = httpContextAccessor;
		this.configurationManager = configurationManager;
		this.environmentManager = environmentManager;
		this.posmHasher = posmHasher;
	}
}
