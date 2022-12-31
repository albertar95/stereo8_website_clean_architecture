using Application.DTO.File;
using Application.DTO.Product;

namespace FrontendUI.ViewModels
{
    public class ProductViewModel
    {
        public DetailProductDto Product { get; set; } = new DetailProductDto();
        public IEnumerable<FileListDto> Files { get; set; } = new List<FileListDto>();
        public IEnumerable<ProductListDto> SimilarProducts { get; set; } = new List<ProductListDto>();

    }
}
