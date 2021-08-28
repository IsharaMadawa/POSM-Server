﻿using HotChocolate;
using HotChocolate.Types;
using POSM_Server.Models;
using System.Linq;

namespace POSM_Server.GraphQL.InvoiceQuery
{
    public class InvoiceType : ObjectType<Invoice>
    {
        // since we are inheriting from objtype we need to override the functionality
        protected override void Configure(IObjectTypeDescriptor<Invoice> descriptor)
        {
            descriptor.Description("Used to group the do Invoice items per Invoice");

            // Ishara[26/08/2021] We can ignore data from here it will exclude from projection
            //descriptor.Field(x => x.InvoiceItems).Ignore();

            descriptor.Field(x => x.InvoiceItems)
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