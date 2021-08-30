using HotChocolate.Types;

namespace POSM.APIs.GraphQLServer.GraphQL.Queries.Users
{
	[ExtendObjectType("Query")]
	public class UserQuery : QueryBase
	{
		public string Welcome()
		{
			return "Welcome To Custom Authentication Servies In GraphQL In Pure Code First";
		}
	}
}
