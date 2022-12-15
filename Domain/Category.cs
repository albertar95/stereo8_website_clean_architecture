using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain 
{
    public class Category : BaseDomainProperties
    {
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Keywords { get; set; }
        public virtual ICollection<Brand> Brands { get; } = new List<Brand>();
        public virtual ICollection<Product> Products { get; } = new List<Product>();
        public virtual ICollection<Type> Types { get; } = new List<Type>();
    }
}
