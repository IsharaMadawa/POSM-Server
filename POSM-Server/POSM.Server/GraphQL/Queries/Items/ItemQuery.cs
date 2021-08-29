using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using POSM.Core.Business.Operations.Interfaces;
using POSM.Core.Data.Db.Models;

namespace POSM.APIs.GraphQLServer.GraphQL.Queries.Items
{
	[ExtendObjectType("Query")]
	public class ItemQuery
	{
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<Item> GetItems([Service] IItemOperator itemOperator)
		{
			return itemOperator.GetItems();
		}
	}
}
