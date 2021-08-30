using HotChocolate;
using HotChocolate.Types;
using POSM.Core.Business.Operations.Interfaces;
using POSM.Core.Bussines.Model.User;

namespace POSM.APIs.GraphQLServer.GraphQL.Mutations.Users
{
	[ExtendObjectType("Mutation")]
	public class UserMutation : MutationBase
	{
		public string Register([Service] IAuthOperator authOperator, UserModel registerInput)
		{
			return authOperator.Register(registerInput);
		}
	}
}
