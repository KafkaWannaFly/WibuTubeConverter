using GetToKnowUWP.ViewModels;
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
using GetToKnowUWP.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GetToKnowUWP.Pages
{
    public sealed partial class MainPage : Page
    {
        MainPageViewModel mainPageViewModel = new MainPageViewModel();
        public MainPage()
        {
            this.InitializeComponent();
            
            //mainPageViewModel = new MainPageViewModel(this.createTabViewItem());      
        }

        private void MainTabView_AddTabButtonClick(TabView sender, object args)
        {
            mainPageViewModel.Add(sender, args);
            //this.MainTabView.TabItems.Add(new TabViewItem());
            //var item = new TabViewItem();
            //item = (TabViewItem) App.Current.Resources["TabItemTemplate"];
            //mainPageViewModel.Add(item);

            //mainPageViewModel.Add(this.createTabViewItem());
            //MainTabView.SelectedItem = mainPageViewModel.MyTabs.Last();
        }


        private void MainTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            //mainPageViewModel.RemoveItem(args.Tab);

            MainTabView.TabItems.Remove(args.Tab);
            mainPageViewModel.RemoveItem((MainPageModel)args.Item);
        }

        TabViewItem createTabViewItem()
        {
            TabViewItem item = new TabViewItem();
            item.Style = (Style)this.Resources["TabHeaderStyle"];
            
            Grid grid = new Grid();
            RowDefinition r0 = new RowDefinition();

            r0.Height = GridLength.Auto;
            grid.RowDefinitions.Add(r0);

            RowDefinition r1 = new RowDefinition();
            r1.Height = new GridLength(1, GridUnitType.Star);
            grid.RowDefinitions.Add(r1);

            Button button = new Button();
            button.Style = (Style) App.Current.Resources["NavigationBackButtonNormalStyle"];
            CommandBar commandBar = new CommandBar();
            commandBar.Background = (Brush) App.Current.Resources.ThemeDictionaries["ApplicationPageBackgroundThemeBrush"];
            commandBar.Content = button;

            grid.Children.Add(commandBar);
            Grid.SetRow(commandBar, 0);

            Frame frame = new Frame();
            frame.Navigate(typeof(UrlSearchPage));

            grid.Children.Add(frame);
            Grid.SetRow(frame, 1);

            item.Content = grid;

            return item;
        }
    }
}