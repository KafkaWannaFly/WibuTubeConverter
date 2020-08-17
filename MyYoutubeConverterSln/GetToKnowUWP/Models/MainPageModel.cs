using GetToKnowUWP.Pages;
using GetToKnowUWP.ViewModels.Commands;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Muxc = Microsoft.UI.Xaml.Controls;
using Wuxc = Windows.UI.Xaml.Controls;

namespace GetToKnowUWP.Models
{
    public class MainPageModel
    {
        string defaultTabName = "New tab";
        Muxc.IconSource defaultIcon = new Muxc.SymbolIconSource() { Symbol = Symbol.NewWindow };
        public string DefaultTabName { get => defaultTabName; }
        public Muxc.IconSource DefaultIcon { get => defaultIcon; }

        Frame frame = new Frame();
        public Frame Frame
        {
            get => frame;
            set
            {
                this.frame = value;
            }
        }
        public CommandEventHandler<object> FlushCacheCommand
        {
            get
            {
                return new CommandEventHandler<object>(async (param) =>
                {
                    await ApplicationData.Current.ClearAsync();
                });
            }
        }

        public CommandEventHandler<object> OpenTempoFolderCommand
        {
            get
            {
                return new CommandEventHandler<object>(async (param) =>
                {
                    await this.OpenTempoFolderAsync();
                });
            }
        }

        public MainPageModel()
        {
            this.frame.Navigate(typeof(UrlSearchPage));
        }

        async Task OpenTempoFolderAsync()
        {
            await Launcher.LaunchFolderAsync(ApplicationData.Current.TemporaryFolder);
        }
    }
}
