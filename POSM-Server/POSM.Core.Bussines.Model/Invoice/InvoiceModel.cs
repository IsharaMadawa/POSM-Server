using System;
using System.Collections.Generic;
using POSM.Core.Bussines.Model.InvoiceItem;

namespace POSM.Core.Bussines.Model.Invoice
{
	 public class InvoiceModel
	{
		public int InvoiceId { get; set; }
		public DateTime InvoiceDateTime { get; set; }

		public IEnumerable<InvoiceItemModel> InvoiceItems { get; set; }
	}
}
