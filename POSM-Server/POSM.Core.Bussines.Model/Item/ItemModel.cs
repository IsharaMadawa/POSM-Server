using System.Collections.Generic;
using POSM.Core.Bussines.Model.InvoiceItem;

namespace POSM.Core.Bussines.Model.Item
{
	public class ItemModel
	{
		public int Id { get; set; }
		public string ItemName { get; set; }
		public string ItemCode { get; set; }
		public decimal UnitPrice { get; set; }

		public IEnumerable<InvoiceItemModel> InvoiceItems { get; set; }
	}
}
