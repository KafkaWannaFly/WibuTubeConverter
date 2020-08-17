using GetToKnowUWP.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetToKnowUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UrlSearchPage : Page
    {
        public UrlSearchViewModel urlSearchViewModel;
        public UrlSearchPage()
        {
            this.InitializeComponent();
            urlSearchViewModel = new UrlSearchViewModel();
            urlSearchViewModel.OnGetVideoCompleted += this.NavigateToMp3Viewer;
        }

        void NavigateToMp3Viewer(FileInfo mp4)
        {
            if(this.Frame != null)
            {
                this.Frame.Navigate(typeof(Mp3ViewerPage), mp4);
            }
        }
    }
}
