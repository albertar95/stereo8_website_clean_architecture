using Application.DTO.File;
using Domain;

namespace FrontendUI.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<Cart> Carts { get; set; } = new List<Cart>();
        public IEnumerable<FileListDto> Files { get; set; } = new List<FileListDto>();
    }
}
