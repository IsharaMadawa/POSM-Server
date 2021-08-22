using System;
using System.Collections.Generic;

#nullable disable

namespace POSM_Server.Models
{
    public partial class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public int? Quantity { get; set; }
        public int? CustomerId { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Item Item { get; set; }
    }
}
