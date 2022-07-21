using CommunityToolkit.Maui.Views;
using WibuTubeConverter.Controls;
using WibuTubeConverter.Pages;

namespace WibuTubeConverter.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string url;

        [ObservableProperty]
        bool isValidUrl;

        readonly WibuTube wibuTube;
        readonly IFileSystem fileSystem;

        public MainPageViewModel(WibuTube wibuTube, IFileSystem fileSystem)
        {
            this.wibuTube = wibuTube;
            this.fileSystem = fileSystem;

            Url = "";
            isValidUrl = false;
        }


        [RelayCommand(CanExecute = nameof(IsValidUrl))]
        async Task SearchVideo(ContentPage mainPage)
        {
            var popUp = new LoadingPopUp();

            var popUpTask = mainPage.ShowPopupAsync(popUp);
            var downloadTask = DownloadVideoAsync();
            var finishedTask = await Task.WhenAny(popUpTask, downloadTask);

            if (finishedTask == downloadTask)
            {
                var videoFileInfo = downloadTask.Result;


                await Shell.Current.GoToAsync(nameof(ConvertPage), new Dictionary<string, object>
                {
                    { nameof(videoFileInfo), videoFileInfo }
                });
                popUp.Close();
            }
        }

        async Task<FileInfo> DownloadVideoAsync()
        {
            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
            {
                var videoFileInfo = await wibuTube.DownloadVideoAsync(Url, fileSystem.CacheDirectory);
                return videoFileInfo;
            }

            return null;
        }
    }
}
