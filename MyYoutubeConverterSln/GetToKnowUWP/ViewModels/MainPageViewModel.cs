using GetToKnowUWP.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GetToKnowUWP.ViewModels
{
    public class MainPageViewModel
    {
        private ObservableCollection<TabViewItem> myTabs = new ObservableCollection<TabViewItem>();
        public ObservableCollection<TabViewItem> MyTabs { get => myTabs; }

        readonly string defaultTabName = "New tab";

        public MainPageViewModel()
        {
            this.Add(defaultTabName);

            //var item = new TabViewItem();
            //item = (TabViewItem)App.Current.Resources["TabItemTemplate"];
            //this.Add(item);
        }

        public MainPageViewModel(TabViewItem firstItem)
        {
            myTabs.Add(firstItem);
        }

        public MainPageViewModel(string firstItem)
        {
            var item = new TabViewItem();
            item.Header = firstItem;

            Frame frame = new Frame();
            item.Content = frame;
            frame.Navigate(typeof(UrlSearchPage));

            myTabs.Add(item);
        }

        public void Add(string itemHeader)
        {
            var item = new TabViewItem();
            
            item.Header = itemHeader;

            Frame frame = new Frame();
            item.Content = frame;
            frame.Navigate(typeof(UrlSearchPage));

            myTabs.Add(item);
        }

        public void Add(TabViewItem item)
        {
            myTabs.Add(item);
        }

        public bool RemoveItem(TabViewItem item)
        {
            return myTabs.Remove(item);
        }
    }
}
