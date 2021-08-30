using HotChocolate;
using HotChocolate.Types;
using POSM.Core.Business.Operations.Interfaces;
using POSM.Core.Bussines.Model.Item;

namespace POSM.APIs.GraphQLServer.GraphQL.Mutations.Items
{
	[ExtendObjectType("Mutation")]
	public class ItemMutation : MutationBase
	{
		public int AddItem([Service] IItemOperator itemOperator, ItemModel itemInput)
		{
			return itemOperator.AddItem(itemInput);
		}
	}
}
