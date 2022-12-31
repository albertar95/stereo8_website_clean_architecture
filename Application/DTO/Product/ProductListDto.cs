using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Product
{
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProductName { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string TypeName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public Guid TypeId { get; set; }
        public decimal Price { get; set; }
        public decimal PriceWithoutOff { get; set; }
        public byte OffPercentage { get; set; }
        public string? Specification { get; set; }
        public int AvailableCount { get; set; }
        public byte? Rating { get; set; }
    }
}
