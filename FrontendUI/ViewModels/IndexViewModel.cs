using Application.DTO.Category;
using Application.DTO.File;
using Application.DTO.Product;

namespace FrontendUI.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CategoryListDto> Categories { get; set; } = new List<CategoryListDto>()!;
        public IEnumerable<ProductListDto> SpecialProducts { get; set; } = new List<ProductListDto>()!;
        public IEnumerable<ProductListDto> OffProducts { get; set; } = new List<ProductListDto>()!;
        public IEnumerable<FileListDto> Files { get; set; } = new List<FileListDto>()!;
    }
}
