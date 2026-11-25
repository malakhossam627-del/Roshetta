using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Order
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string Method { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime? PaidAt { get; set; }

        public virtual CustomerOrder Order { get; set; } = null!;
    }
}
