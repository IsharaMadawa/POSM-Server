using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using POSM.Fx.Utilities.Constants;

namespace POSM.APIs.GraphQLServer.GraphQL.Queries.Users
{
	[ExtendObjectType("Query")]
	public class UserQuery : QueryBase
	{
		[Authorize(Policy = PolicyConstants.POLICY_INTERNAL_ADMIN)]
		public string Welcome()
		{
			return "Welcome To Custom Authentication Servies In GraphQL In Pure Code First";
		}
	}
}
