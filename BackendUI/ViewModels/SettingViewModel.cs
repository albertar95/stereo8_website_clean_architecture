using Domain;

namespace BackendUI.ViewModels
{
    public class SettingViewModel
    {
        public IEnumerable<Setting> Settings { get; set; } = new List<Setting>()!;
        public IEnumerable<Domain.File> Files { get; set; } = new List<Domain.File>()!;
    }
}
