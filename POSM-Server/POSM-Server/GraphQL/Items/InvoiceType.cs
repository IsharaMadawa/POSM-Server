using HotChocolate;
using HotChocolate.Types;
using POSM_Server.Models;
using System.Linq;

namespace POSM_Server.GraphQL.Items
{
    public class InvoiceType : ObjectType<Invoice>
    {
        // since we are inheriting from objtype we need to override the functionality
        protected override void Configure(IObjectTypeDescriptor<Invoice> descriptor)
        {
            descriptor.Description("Used to group the do Invoice items per Invoice");

            descriptor.Field(x => x.InvoiceId).Ignore();

            descriptor.Field(x => x.InvoiceId)
                        .ResolveWith<Resolvers>(p => p.GetInvoiceItems(default!, default!))
                        .UseDbContext<POSMContext>()
                        .Description("This is the list of to do item available for particuler Invoice");
        }

        private class Resolvers
        {
            public IQueryable<InvoiceItem> GetInvoiceItems(Invoice invoice, [ScopedService] POSMContext context)
            {
                return context.InvoiceItems.Where(x => x.InvoiceId == invoice.InvoiceId);
            }
        }
    }
}