using Application.DTO.File;
using Domain;

namespace FrontendUI.ViewModels
{
    public class FavoriteViewModel
    {
        public IEnumerable<Favorite> Favorites { get; set; } = new List<Favorite>();
        public IEnumerable<FileListDto> Files { get; set; } = new List<FileListDto>();
    }
}
