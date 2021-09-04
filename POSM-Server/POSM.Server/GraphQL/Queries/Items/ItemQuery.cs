using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using POSM.Core.Business.Operations.Interfaces;
using POSM.Core.Data.Db.Models;
using POSM.Fx.Utilities.Constants;

namespace POSM.APIs.GraphQLServer.GraphQL.Queries.Items
{
	[ExtendObjectType("Query")]
	public class ItemQuery : QueryBase
	{
		[Authorize(Policy = PolicyConstants.POLICY_INTERNAL_CASHIER)]
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<Item> GetItems([Service] IItemOperator itemOperator)
		{
			return itemOperator.GetItems();
		}
	}
}
