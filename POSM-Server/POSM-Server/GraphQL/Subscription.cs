using HotChocolate;
using HotChocolate.Types;
using POSM_Server.Models;

namespace POSM_Server.GraphQL
{
	public class Subscription
	{
		[Subscribe]
		[Topic]
		public Invoice OnItemAdded([EventMessage] Invoice invoice) => invoice;
	}
}
