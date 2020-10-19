using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using WibuTubeConverter.Models;

namespace WibuTubeConverter.ViewModels
{
    public class MainPageViewModel
    {
        private ObservableCollection<MainPageModel> myTabs = new ObservableCollection<MainPageModel>();
        public MainPageViewModel()
        {
            myTabs.Add(new MainPageModel());
        }

        public ObservableCollection<MainPageModel> MyTabs { get => myTabs; }
        public void Add(TabView sender, object args)
        {
            myTabs.Add(new MainPageModel());
        }

        public bool RemoveItem(MainPageModel item)
        {
            return this.myTabs.Remove(item);
        }
    }
}