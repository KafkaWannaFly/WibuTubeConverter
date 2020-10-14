using WibuTubeConverter.ViewModels;
using WibuTubeConverter.ViewModels.Commands;
using System;
using System.IO;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Input;
using GetToKnowUWP.Services;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WibuTubeConverter.Pages
{
    
    public sealed partial class Mp3ViewerPage : Page
    {
        Mp3ViewerViewModel mp3ViewerViewModel = new Mp3ViewerViewModel();
        public Mp3ViewerPage()
        {
            this.InitializeComponent();
            SnapshotSlider.ValueChanged += SnapshotSlider_ValueChanged;

            SnapshotSlider.ValueChanged += (sender, args) =>
            {
                SaveButton.Content = SnapshotSlider.Value;
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                base.OnNavigatedTo(e);

                FileInfo mp4 = (FileInfo)e.Parameter;
                if (mp4 != null)
                {
                    this.mp3ViewerViewModel.Init(mp4);
                }

            }
            catch (System.Exception ex)
            {
                var mess = new MessageDialog(ex.Message);
                var awt = mess.ShowAsync().GetAwaiter().GetResult();
            }
            
        }

        private async void SnapshotSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            try
            {
                await this.mp3ViewerViewModel.UpdateSnapshotPreview(SnapshotSlider.Value);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }
    }
}