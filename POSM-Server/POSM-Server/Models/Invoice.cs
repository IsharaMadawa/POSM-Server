using System;
using System.Collections.Generic;

#nullable disable

namespace POSM_Server.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int InvoiceId { get; set; }
        public DateTime InvoiceDateTime { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
