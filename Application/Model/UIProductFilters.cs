using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class UIProductFilters
    {
        public Guid NidCategory { get; set; }
        public List<Guid> TypeIds { get; set; }
        public List<Guid> BrandIds { get; set; }
        public decimal MinPrice { get; set; } = 0!;
        public decimal MaxPrice { get; set; } = 10000000!;
        public int SortType { get; set; }
        public int Pagesize { get; set; } = 10!;
        public int Skip { get; set; } = 0!;
    }
}
