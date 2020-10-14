using WibuTubeConverter.Models;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace WibuTubeConverter.ViewModels
{
    public class MainPageViewModel
    {
        private ObservableCollection<MainPageModel> myTabs = new ObservableCollection<MainPageModel>();
        public ObservableCollection<MainPageModel> MyTabs { get => myTabs; }
        
        public MainPageViewModel()
        {
            myTabs.Add(new MainPageModel());
        }

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
