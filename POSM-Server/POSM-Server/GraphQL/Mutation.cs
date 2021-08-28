using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using POSM_Server.GraphQL.InvoiceQuery;
using POSM_Server.GraphQL.ItemQuery;
using POSM_Server.Models;

namespace POSM_Server.GraphQL
{
	public class Mutation
	{
        // this attribute will help us utilise the multi threaded api db context
        [UseDbContext(typeof(POSMContext))]
        public async Task<AddInvoicePayload> AddInvoiceAsync(AddInvoiceInput input, [ScopedService] POSMContext context, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            List<InvoiceItem> itemList = new List<InvoiceItem>();
            foreach (InvoiceItem item in input.invoice.InvoiceItems)
            {
                itemList.Add(new InvoiceItem { ItemId = item.ItemId, Quantity = item.Quantity, CustomerId = item.CustomerId });
            }

            var newInvoice = new Invoice
            {
                InvoiceDateTime = input.invoice.InvoiceDateTime,
                InvoiceItems = itemList
            };

            context.Invoices.Add(newInvoice);
            await context.SaveChangesAsync();

            // we emit our subscription
            await eventSender.SendAsync(nameof(Subscription.OnItemAdded), newInvoice, cancellationToken);

            return new AddInvoicePayload(newInvoice);
        }

        [UseDbContext(typeof(POSMContext))]
        public async Task<int> AddItemAsync(AddItemInput input, [ScopedService] POSMContext context)
        {
            var item = new Item
            {
                ItemCode = input.itemData.ItemCode,
                ItemName = input.itemData.ItemName,
                UnitPrice = input.itemData.UnitPrice
            };

            context.Items.Add(item);
            await context.SaveChangesAsync();

            return item.Id;
        }
    }
}
