using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WibuTubeConverter.Controls;

namespace WibuTubeConverter.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string url;

        private readonly WibuTube.WibuTubeConverter wibuTubeConverter;

        public MainPageViewModel(WibuTube.WibuTubeConverter wibuTubeConverter)
        {
            this.wibuTubeConverter = wibuTubeConverter;
        }

        [RelayCommand]
        async Task SearchVideo(ContentPage mainPage)
        {
            var popUp = new LoadingPopUp();

            var popUpTask = mainPage.ShowPopupAsync(popUp);
            var myTask = myExampleTask();
            var finishedTask = await Task.WhenAny(popUpTask, myTask);

            if (finishedTask == myTask)
            {
                popUp.Close();
            }
        }

        async Task<string> myExampleTask()
        {
            await Task.Delay(3000);
            return "Hello";
        }
    }
}
