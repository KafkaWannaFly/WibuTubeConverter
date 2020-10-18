using WibuTubeConverter.Pages;
using WibuTubeConverter.ViewModels.Commands;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Muxc = Microsoft.UI.Xaml.Controls;
using Wuxc = Windows.UI.Xaml.Controls;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WibuTubeConverter.Models
{
    public class MainPageModel: INotifyPropertyChanged
    {
        string defaultTabName = "New tab";
        Muxc.IconSource defaultIcon = new Muxc.SymbolIconSource() { Symbol = Symbol.NewWindow };
        public string DefaultTabName 
        { 
            get => defaultTabName;
            set
            {
                defaultTabName = value;
                OnPropertyChanged();
            }
        }
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

        public CommandEventHandler<object> GoForwardCommand
        {
            get
            {
                return new CommandEventHandler<object>((obj) =>
                {
                    if (frame.CanGoForward)
                    {
                        frame.GoForward();
                    }
                });
            }
        }

        public CommandEventHandler<object> GoBackCommand
        {
            get
            {
                return new CommandEventHandler<object>((obj) =>
                {
                    if(frame.CanGoBack)
                    {
                        frame.GoBack();
                    }
                });
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
            frame.Navigate(typeof(UrlSearchPage));

            UrlSearchPage searchPage = (UrlSearchPage) frame.Content;
            if(searchPage != null)
            {
                searchPage.urlSearchViewModel.OnGetVideoCompleted += setTabHeaderToMp4Name;
            }
        }

        async Task OpenTempoFolderAsync()
        {
            await Launcher.LaunchFolderAsync(ApplicationData.Current.TemporaryFolder);
        }

        private void setTabHeaderToMp4Name(FileInfo mp4)
        {
            DefaultTabName = mp4.Name;
        }

        #region Implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
