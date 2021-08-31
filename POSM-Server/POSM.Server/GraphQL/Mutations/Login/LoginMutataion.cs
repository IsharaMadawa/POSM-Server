using HotChocolate;
using HotChocolate.Types;
using POSM.Core.Business.Operations.Interfaces;
using POSM.Core.Bussines.Model.Login;

namespace POSM.APIs.GraphQLServer.GraphQL.Mutations.Login;

[ExtendObjectType("Mutation")]
public class LoginMutataion : MutationBase
{
	public string Login([Service] IAuthOperator authOperator, LoginModel loginInput)
	{
		return authOperator.Login(loginInput);
	}
}
