using System;
using System.Collections.Generic;

#nullable disable

namespace POSM.Core.Data.Db.Models
{
    public partial class Item
    {
        public Item()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
