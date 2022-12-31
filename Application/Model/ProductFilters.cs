using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class ProductFilters
    {
        public int FilterType { get; set; }
        public Guid CategoryId { get; set; }
        public Guid TypeId { get; set; }
        public Guid BrandId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal FromPrice { get; set; }
        public decimal ToPrice { get; set; }
        public int AvailableCount { get; set; }
    }
}
