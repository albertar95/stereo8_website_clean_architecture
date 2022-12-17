using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Brand
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public string BrandName { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }
    }
}
