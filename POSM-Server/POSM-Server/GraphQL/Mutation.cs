using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using POSM_Server.Models;

namespace POSM_Server.GraphQL
{
	public class Mutation
	{
        // this attribute will help us utilise the multi threaded api db context
        [UseDbContext(typeof(POSMContext))]
        public async Task<AddInvoicePayload> AddInvoiceAsync(AddInvoiceInput input, [ScopedService] POSMContext context)
        {
            List<InvoiceItem> itemList = new List<InvoiceItem>();
            itemList.Add(new InvoiceItem { ItemId = input.item.Id});

            var invoice = new Invoice
            {
                InvoiceDateTime = DateTime.Now,
                InvoiceItems = itemList
            };

            context.Invoices.Add(invoice);
            await context.SaveChangesAsync();

            return new AddInvoicePayload(invoice);
        }
    }
}
