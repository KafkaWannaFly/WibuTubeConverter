using WibuTubeConverter.ViewModels;
using System;
using System.IO;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;


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
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                base.OnNavigatedTo(e);

                FileInfo mp4 = (FileInfo)e.Parameter;
                if (mp4 != null)
                {
                    await mp3ViewerViewModel.InitAsync(mp4);
                }
                else
                {
                    await new MessageDialog($"{nameof(Mp3ViewerPage)}: Can't access to mp4 anymore").ShowAsync();

                    Frame.GoBack(e.NavigationTransitionInfo);
                }
            }
            catch (System.Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            
        }

        private async void SnapshotSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            try
            {
                Slider slider = (Slider)sender;
                if (slider != null)
                {
                    await this.mp3ViewerViewModel.UpdateSnapshotPreview(slider.Value);
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }
    }
}