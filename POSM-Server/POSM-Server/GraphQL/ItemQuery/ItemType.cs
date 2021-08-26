using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using POSM_Server.Models;

namespace POSM_Server.GraphQL.ItemQuery
{
    public class ItemType : ObjectType<Item>
    {
        // since we are inheriting from objtype we need to override the functionality
        protected override void Configure(IObjectTypeDescriptor<Item> descriptor)
        {
            descriptor.Description("Used to define item for a invoces list");

            descriptor.Field(x => x.InvoiceItems)
                        .ResolveWith<Resolvers>(p => p.GetInvoicesThatItemBelongs(default!, default!))
                        .UseDbContext<POSMContext>()
                        .Description("To get list of invoices that item belongs Item Id");
        }

        private class Resolvers
        {
            public IQueryable<Invoice> GetInvoicesThatItemBelongs(int itemId, [ScopedService] POSMContext context)
            {
                return context.Invoices.Where(i => i.InvoiceItems.Any(t => t.ItemId == itemId));
            }
        }
    }
}
