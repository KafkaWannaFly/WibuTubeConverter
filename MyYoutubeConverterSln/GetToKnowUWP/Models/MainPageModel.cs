using GetToKnowUWP.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Muxc = Microsoft.UI.Xaml.Controls;
using Wuxc = Windows.UI.Xaml.Controls;

namespace GetToKnowUWP.Models
{
    public class MainPageModel
    {
        string defaultTabName = "New tab";
        Muxc.IconSource defaultIcon = new Muxc.SymbolIconSource() { Symbol = Symbol.NewWindow };
        Page tabPage = new UrlSearchPage();
        public string DefaultTabName { get => defaultTabName; }
        public Muxc.IconSource DefaultIcon { get => defaultIcon; }
        public Page TabPage { get => tabPage; }
    }
}
