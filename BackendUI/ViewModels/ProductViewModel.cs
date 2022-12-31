using Application.DTO.Category;
using Application.DTO.File;
using Application.DTO.Product;

namespace BackendUI.ViewModels
{
    public class ProductViewModel
    {
        public ProductDto Product { get; set; } = new ProductDto()!;
        public IEnumerable<CategoryListDto> Categories { get; set; } = new List<CategoryListDto>()!;
        public IEnumerable<ProductListDto> Products { get; set; } = new List<ProductListDto>()!;
        public IEnumerable<FileListDto> Files { get; set; } = new List<FileListDto>()!;
    }
}
