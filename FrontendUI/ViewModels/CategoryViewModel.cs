using Application.DTO.Category;
using Application.DTO.File;
using Application.DTO.Product;

namespace FrontendUI.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryDto Category { get; set; } = new CategoryDto();
        public IEnumerable<ProductListDto> Products { get; set; } = new List<ProductListDto>();
        public IEnumerable<FileListDto> Files { get; set; } = new List<FileListDto>();
        public int ProductCount { get; set; }
        public int PageNumber { get; set; }
        public int SortType { get; set; }
        public Guid BrandId { get; set; } = Guid.Empty!;
        public Guid TypeId { get; set; } = Guid.Empty!;
    }
}
