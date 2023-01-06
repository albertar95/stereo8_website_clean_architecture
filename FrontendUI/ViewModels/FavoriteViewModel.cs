using Application.DTO.Favorite;
using Application.DTO.File;
using Domain;

namespace FrontendUI.ViewModels
{
    public class FavoriteViewModel
    {
        public IEnumerable<FavoriteListDto> Favorites { get; set; } = new List<FavoriteListDto>();
        public IEnumerable<FileListDto> Files { get; set; } = new List<FileListDto>();
    }
}
