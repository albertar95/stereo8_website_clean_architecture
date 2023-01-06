using Application.DTO.Cart;
using Application.DTO.File;
using Domain;

namespace FrontendUI.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<CartListDto> Carts { get; set; } = new List<CartListDto>();
        public IEnumerable<FileListDto> Files { get; set; } = new List<FileListDto>();
    }
}
