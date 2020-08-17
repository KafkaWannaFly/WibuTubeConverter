using System.Collections.ObjectModel;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetToKnowUWP.Pages
{

    public sealed partial class Mp3ViewerPage : Page
    {
        public Mp3ViewerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FileInfo mp4 = (FileInfo) e.Parameter;
            if(mp4 != null)
            {
                this.textBlock.Text = $"Video source:\n{mp4.FullName}";
            }
            base.OnNavigatedTo(e);
        }
    }
}