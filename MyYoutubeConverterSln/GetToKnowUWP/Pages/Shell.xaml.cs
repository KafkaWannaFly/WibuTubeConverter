using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetToKnowUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Page
    {
        public Shell()
        {
            this.InitializeComponent();

            if(MainFrame.Content == null)
            {
                this.MainFrame.Navigate(typeof(MainPage));
                this.navigationView.SelectedItem = this.MainPageTab;
            }
        }

        private void navigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem viewItem = (NavigationViewItem)sender.SelectedItem;
            if (viewItem.Name == this.MainPageTab.Name)
            {
                this.MainFrame.Navigate(typeof(MainPage), args);
            }
            else if (viewItem.Name == this.Mp3ViewerTab.Name)
            {
                this.MainFrame.Navigate(typeof(Mp3ViewerPage), args);
            }
        }

    }
}