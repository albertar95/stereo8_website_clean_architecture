using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Product
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public Guid TypeId { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }
        public decimal Price { get; set; }
        public decimal PriceWithoutOff { get; set; }
        public byte Priority { get; set; }
        public byte OffPercentage { get; set; }
        public int AvailableCount { get; set; }
        public string? Specification { get; set; }
        public string? DetailDesc { get; set; }
        public Guid UserId { get; set; }
        public byte? Rating { get; set; }
        public double? Weight { get; set; }
    }
}
