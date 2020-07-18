using GetToKnowUWP.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using SymbolIconSource = Microsoft.UI.Xaml.Controls.SymbolIconSource;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GetToKnowUWP.Pages
{
    public sealed partial class MainPage : Page
    {
        MainPageViewModel mainPageViewModel;
        public MainPage()
        {
            this.InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
        }

        private void MainTabView_AddTabButtonClick(TabView sender, object args)
        {
            mainPageViewModel.Add("New tab");

            //var item = new TabViewItem();
            //item = (TabViewItem) App.Current.Resources["TabItemTemplate"];
            //mainPageViewModel.Add(item);

            MainTabView.SelectedItem = mainPageViewModel.MyTabs.Last();
        }


        private void MainTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            mainPageViewModel.RemoveItem(args.Tab);
        }
    }
}