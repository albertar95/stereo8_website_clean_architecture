using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain 
{
    public class Ship : BaseDomainProperties
    {
        public DateTime? DueDate { get; set; }
        public string? PersianDueDate { get; set; }
        public byte ShipVia { get; set; }
        public string Address { get; set; } = null!;
        public decimal ZipCode { get; set; }
        public decimal ShipPrice { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
    }
}
