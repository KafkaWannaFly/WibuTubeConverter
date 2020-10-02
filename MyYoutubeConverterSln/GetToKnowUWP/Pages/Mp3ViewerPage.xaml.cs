using GetToKnowUWP.ViewModels;
using GetToKnowUWP.ViewModels.Commands;
using System;
using System.IO;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetToKnowUWP.Pages
{
    
    public sealed partial class Mp3ViewerPage : Page
    {
        Mp3ViewerViewModel mp3ViewerViewModel = new Mp3ViewerViewModel();
        public Mp3ViewerPage()
        {
            this.InitializeComponent();
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

        CommandEventHandler<bool> UseSnapshotCmd
        {
            get
            {
                return mp3ViewerViewModel.mp3ViewerModel.UseSnapshotCommand;
            }
        }
    }
}