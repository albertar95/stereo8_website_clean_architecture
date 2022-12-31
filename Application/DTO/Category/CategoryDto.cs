using Application.DTO.Brand;
using Application.DTO.Product;
using Application.DTO.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Keywords { get; set; }
        public ICollection<BrandListDto> Brands { get; } = new List<BrandListDto>();
        public ICollection<TypeListDto> Types { get; } = new List<TypeListDto>();
        public ICollection<ProductListDto> Products { get; } = new List<ProductListDto>();
    }
}
