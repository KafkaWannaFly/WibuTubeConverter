using Core.TryYoutubeApi;
using WibuTubeConverter.Models;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;
using Windows.UI.Popups;
using FFmpegInterop;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.FileProperties;
using Windows.Media.Editing;
using Windows.Graphics.Imaging;
using WibuTubeConverter.ViewModels.Commands;
using Windows.ApplicationModel.Store;
using Windows.UI.Xaml.Media.Imaging;

namespace WibuTubeConverter.ViewModels
{
    public class Mp3ViewerViewModel: INotifyPropertyChanged
    {
        public Mp3ViewerModel mp3ViewerModel = new Mp3ViewerModel();
        YoutubeConverter youtubeConverter = new YoutubeConverter();
        TagLib.File mediaDetail;
        string mp4Path;
        StorageFile mp4;
        FFmpegInteropMSS ffmpegInteropMSS;
        public Mp3ViewerViewModel()
        {
            
        }
        
        public CommandEventHandler<double> UpdateSnapshotCmd
        {
            get
            {
                return new CommandEventHandler<double>(
                    async (second) => await UpdateSnapshotPreview(second)
                    );
            }
        }

        public void Init(FileInfo _mp4)
        {
            mp4Path = _mp4.FullName;
            this.mediaDetail = TagLib.File.Create(_mp4.FullName);

            this.mp3ViewerModel.Tittle = Path.GetFileNameWithoutExtension(_mp4.Name);
            this.mp3ViewerModel.Duration = this.mediaDetail.Properties.Duration.TotalSeconds;

        }

        public async Task UpdateSnapshotPreview(double second)
        {
            try
            {
                if (mp4 == null)
                {
                    mp4 = await StorageFile.GetFileFromPathAsync(mp4Path);
                }

                var imgName = Path.ChangeExtension(mp4.Name, ".png");

                if (ffmpegInteropMSS == null)
                {
                    ffmpegInteropMSS = await FFmpegInteropMSS.CreateFromStreamAsync(await mp4.OpenAsync(FileAccessMode.ReadWrite));
                }

                StorageFile imgFile = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(imgName, CreationCollisionOption.GenerateUniqueName);

                MediaComposition mediaComposition = new MediaComposition();
                MediaClip mediaClip = await MediaClip.CreateFromFileAsync(mp4);
                mediaComposition.Clips.Add(mediaClip);
                
                var interval = TimeSpan.FromSeconds(second);
                var thumbnail = await mediaComposition.GetThumbnailAsync(interval, 
                    1024, 768, VideoFramePrecision.NearestKeyFrame);

                using (var randomAccess = await imgFile.OpenAsync(FileAccessMode.ReadWrite))
                using (var destStream = randomAccess.AsStream())
                using (var sourceStream = thumbnail.AsStream())
                {
                    await sourceStream.CopyToAsync(destStream);
                }

                mp3ViewerModel.ImagePath = imgFile.Path;

                //mp3ViewerModel.ThumbnailImage = new BitmapImage(new Uri(imgFile.Path));
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
