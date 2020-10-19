using WibuTubeConverter.Services;
using WibuTubeConverter.ViewModels.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using VideoLibrary;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Popups;

namespace WibuTubeConverter.ViewModels
{
    public class UrlSearchViewModel: INotifyPropertyChanged
    {
        YoutubeConverter youtubeConverter = new YoutubeConverter();

        public UrlSearchViewModel()
        {

        }

        /// <summary>
        /// Invoked when video is completely downloaded.
        /// Param is FileInfo of that video
        /// </summary>
        public Action<FileInfo> OnGetVideoCompleted;
        public CommandEventHandler<string> GetVideo
        {
            get
            {
                return new CommandEventHandler<string>(async (url) => 
                { 
                    FileInfo mp4 = await this.getVideo(url);
                    if (mp4 != null)
                    {
                        this.OnGetVideoCompleted.Invoke(mp4);
                    }
                });
            }
        }

        private async Task<FileInfo> getVideo(string url)
        {
            try
            {
                YouTubeVideo video = await this.youtubeConverter.GetVideoAsync(url);

                StorageFolder folder = App.TemporaryFolder;

                //StorageFolder folder = await DownloadsFolder.CreateFolderAsync(YoutubeConverter.TemporaryFolder, CreationCollisionOption.OpenIfExists);

                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                StorageFile storageFile = await folder.CreateFileAsync(video.FullName, CreationCollisionOption.OpenIfExists);

                using (Stream source = await video.StreamAsync())
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

                return new FileInfo(storageFile.Path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await new MessageDialog(ex.Message).ShowAsync();
            }

            return null;
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
