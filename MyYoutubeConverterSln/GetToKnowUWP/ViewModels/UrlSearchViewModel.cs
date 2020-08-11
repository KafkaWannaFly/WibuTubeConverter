using Core.TryYoutubeApi;
using GetToKnowUWP.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoLibrary;
using Windows.ApplicationModel;
using Windows.Security.Cryptography;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace GetToKnowUWP.ViewModels
{
    public class UrlSearchViewModel: INotifyPropertyChanged
    {
        private string youtubeURL;
        public string YoutubeURL
        {
            get => youtubeURL;
            set
            {
                if(youtubeURL != value)
                {
                    this.youtubeURL = value;
                    this.OnPropertyChanged();
                }
            }
        }

        YoutubeConverter youtubeConverter = new YoutubeConverter();
        public CommadEventHandler<string> GetVideo
        {
            get
            {
                return new CommadEventHandler<string>(async (s) => { await this.getVideo(s); });
            }
        }

        private async Task<FileInfo> getVideo(string url)
        {
            try
            {
                //var folderPicker = new Windows.Storage.Pickers.FolderPicker();
                //folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
                //folderPicker.FileTypeFilter.Add("*");

                //StorageFolder folder = await folderPicker.PickSingleFolderAsync();

                YouTubeVideo video = await this.youtubeConverter.GetVideoAsync(url);

                StorageFolder folder = ApplicationData.Current.TemporaryFolder;
                folder = await folder.CreateFolderAsync(YoutubeConverter.TemporaryFolder, CreationCollisionOption.OpenIfExists);
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                StorageFile storageFile = await folder.CreateFileAsync(video.FullName, CreationCollisionOption.ReplaceExisting);
                using(Stream source = await video.StreamAsync())
                {
                    using(var randomAccessStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        using(var dest = randomAccessStream.AsStream())
                        {
                            await source.CopyToAsync(dest);
                        }
                    }
                }

                await new MessageDialog($"Write to: {storageFile.Path}").ShowAsync();

                //return await youtubeConverter.DownloadVideoAsync(url, folder.Path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await new MessageDialog(ex.Message).ShowAsync();
            }

            return null;
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
