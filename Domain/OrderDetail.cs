using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain 
{
    public class OrderDetail : BaseDomainProperties
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
