using WibuTubeConverter.Constants;

namespace WibuTubeConverter.ViewModels
{
    [QueryProperty(nameof(VideoFileInfo), nameof(NavigationParamConstants.VideoFileInfo))]
    public partial class ConvertViewModel : ObservableObject
    {
        public FileInfo VideoFileInfo { get; set; }
    }
}
