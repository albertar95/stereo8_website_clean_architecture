using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain 
{
    public class Type : BaseDomainProperties
    {
        public string TypeName { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Product> Products { get; } = new List<Product>();
    }
}

