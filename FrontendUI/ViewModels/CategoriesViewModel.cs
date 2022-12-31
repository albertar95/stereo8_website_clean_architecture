using Application.DTO.Category;
using Application.DTO.File;

namespace FrontendUI.ViewModels
{
    public class CategoriesViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public IEnumerable<FileListDto> Files { get; set; } = new List<FileListDto>();
    }
}
