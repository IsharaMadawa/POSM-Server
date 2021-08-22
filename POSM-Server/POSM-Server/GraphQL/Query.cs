using HotChocolate;
using HotChocolate.Data;
using POSM_Server.Models;
using System.Linq;

namespace POSM_Server.GraphQL
{
	public class Query
	{
		[UseDbContext(typeof(POSMContext))]
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<Item> GetItems([ScopedService] POSMContext context)
		{
			return context.Items;
		}

		[UseDbContext(typeof(POSMContext))]
		[UseProjection] 
		[UseFiltering]
		[UseSorting]
		public IQueryable<Invoice> Invoices([ScopedService] POSMContext context)
		{
			return context.Invoices;
		}
	}
}
