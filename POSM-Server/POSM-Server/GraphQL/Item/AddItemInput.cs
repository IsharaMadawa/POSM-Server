using POSM_Server.Models;

namespace POSM_Server.GraphQL
{	
	public record AddInvoiceInput(Item item);

	public record AddInvoicePayload(Invoice list);
}
