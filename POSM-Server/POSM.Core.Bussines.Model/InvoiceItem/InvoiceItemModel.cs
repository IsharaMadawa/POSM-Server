using POSM.Core.Bussines.Model.Invoice;
using POSM.Core.Bussines.Model.Item;

namespace POSM.Core.Bussines.Model.InvoiceItem
{
	public class InvoiceItemModel
	{
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public int? Quantity { get; set; }
        public int? CustomerId { get; set; }

        public InvoiceModel Invoice { get; set; }
        public ItemModel Item { get; set; }
    }
}
