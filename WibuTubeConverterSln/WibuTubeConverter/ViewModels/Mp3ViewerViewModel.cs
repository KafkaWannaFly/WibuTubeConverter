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
using System.Collections.Generic;
using Windows.System.Threading;

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

        Queue<IStorageFile> deletedLater = new Queue<IStorageFile>();
        ThreadPoolTimer periodicCleaner = null;

        public Mp3ViewerViewModel()
        {
            this.periodicCleaner = this.periodicThumbnailImageCleaner(1, deletedLater);
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

                var imgName = Path.ChangeExtension(mp4.Name, ".jpg");

                if (ffmpegInteropMSS == null)
                {
                    ffmpegInteropMSS = await FFmpegInteropMSS.CreateFromStreamAsync(await mp4.OpenAsync(FileAccessMode.ReadWrite));
                }

                StorageFolder tmpFolder = App.TemporaryFolder;

                StorageFile oldImg = (StorageFile)await tmpFolder.TryGetItemAsync(imgName);
                if(oldImg != null)
                {
                    deletedLater.Enqueue(oldImg);
                }

                StorageFile imgFile = await tmpFolder.CreateFileAsync(imgName, CreationCollisionOption.GenerateUniqueName);

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
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            
        }

        /// <summary>
        /// To do preview snapshot function, we have to create a bunch of image files each time slider value changed
        /// <br/>
        /// So, this cleaner guy will go checking and deleting un-used images
        /// </summary>
        /// <param name="eachSecond">Time between each clean</param>
        /// <param name="images">List of images which are going to be deleted forever</param>
        /// <returns>ThreadPoolTimer that represents the task</returns>
        ThreadPoolTimer periodicThumbnailImageCleaner(int eachSecond, Queue<IStorageFile> images)
        {
            TimeSpan interval = TimeSpan.FromSeconds(eachSecond * 1000);
            ThreadPoolTimer cleaner = ThreadPoolTimer.CreatePeriodicTimer(
                async (source) =>
                {
                    Queue<Task> deleting = new Queue<Task>();
                    while (images.Count > 0)
                    {
                        var img = images.Dequeue();
                        deleting.Enqueue(
                            Task.Run(
                                async () => await img.DeleteAsync(StorageDeleteOption.PermanentDelete)
                                )
                            );
                    }
                    await Task.WhenAll(deleting);
                }, interval);

            return cleaner;
        }

        ~Mp3ViewerViewModel()
        {
            if(periodicCleaner != null)
            {
                periodicCleaner.Cancel();
            }
        }

        #region PropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
