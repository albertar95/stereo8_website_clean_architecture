using Application.DTO.File;
using Domain;

namespace BackendUI.ViewModels
{
    public class SettingViewModel
    {
        public IEnumerable<Setting> Settings { get; set; } = new List<Setting>()!;
        public IEnumerable<FileListDto> Files { get; set; } = new List<FileListDto>()!;
    }
}
