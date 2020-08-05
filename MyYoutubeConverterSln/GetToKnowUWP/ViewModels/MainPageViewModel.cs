using GetToKnowUWP.Models;
using GetToKnowUWP.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Muxc = Microsoft.UI.Xaml.Controls;

namespace GetToKnowUWP.ViewModels
{
    public class MainPageViewModel
    {
        private ObservableCollection<MainPageModel> myTabs = new ObservableCollection<MainPageModel>();
        public ObservableCollection<MainPageModel> MyTabs { get => myTabs; }

            

        public MainPageViewModel()
        {
            myTabs.Add(new MainPageModel());
        }

        public MainPageViewModel(TabViewItem item)
        {
            
        }

        public void Add(TabView sender, object args)
        {
            myTabs.Add(new MainPageModel());
        }

        public bool RemoveItem(MainPageModel item)
        {
            return this.myTabs.Remove(item);
            //return this.myTabs.Remove(myTabs.FirstOrDefault(x => x.GetHashCode() == item.GetHashCode()));
            //return false;
        }
    }
}
