using Application.DTO.Category;
using Application.DTO.File;
using Domain;

namespace BackendUI.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryDto Category { get; set; } = new CategoryDto()!;
        public List<FileListDto> Files { get; set; } = new List<FileListDto>()!;
    }
}
