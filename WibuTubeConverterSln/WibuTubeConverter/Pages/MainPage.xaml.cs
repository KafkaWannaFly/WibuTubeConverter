using WibuTubeConverter.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Muxc = Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.Gaming.XboxLive.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Wuxc = Windows.UI.Xaml.Controls;
using WibuTubeConverter.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WibuTubeConverter.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel mainPageViewModel = new MainPageViewModel();
        public MainPage()
        {
            this.InitializeComponent();            
        }

        private void MainTabView_AddTabButtonClick(TabView sender, object args)
        {
            mainPageViewModel.Add(sender, args);
        }

        private void MainTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            MainTabView.TabItems.Remove(args.Tab);
            mainPageViewModel.RemoveItem((MainPageModel)args.Item);
        }
    }
}