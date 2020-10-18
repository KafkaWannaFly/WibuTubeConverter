using WibuTubeConverter.Models;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;
using Windows.UI.Popups;
using Windows.Storage;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.FileProperties;
using Windows.Media.Editing;
using WibuTubeConverter.ViewModels.Commands;
using System.Collections.Generic;
using Windows.System.Threading;
using Windows.Storage.Pickers;
using Windows.Media.MediaProperties;
using Windows.Media.Transcoding;

namespace WibuTubeConverter.ViewModels
{
    public class Mp3ViewerViewModel
    {
        public Mp3ViewerModel mp3ViewerModel = new Mp3ViewerModel();

        string mp4Path;
        StorageFile mp4;

        public Mp3ViewerViewModel()
        {

        }
        
        public CommandEventHandler<object> SaveAsButtonCmd
        {
            get
            {
                return new CommandEventHandler<object>(async (obj) => await SaveButtonClicked(),
                    () => !String.IsNullOrEmpty(mp3ViewerModel.Tittle));
            }
        }

        private async Task SaveButtonClicked()
        {
            try
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.CommitButtonText = "Save as";
                savePicker.SuggestedFileName = mp3ViewerModel.Tittle;
                savePicker.FileTypeChoices.Add("Audio", new List<string> { ".mp3" });
                StorageFile mp3 = await savePicker.PickSaveFileAsync();
                if(mp3 == null)
                {
                    return;
                }

                //Some functions got "Access Denied", so we have to write to this place and copy the chosen later
                StorageFile pseudoMp3 = await App.TemporaryFolder.CreateFileAsync(mp3.Name);
                pseudoMp3 = await ConvertToMp3(mp4, pseudoMp3);

                setThumbnail(pseudoMp3.Path, mp3ViewerModel.ImagePath);

                MusicProperties musicProperties = await pseudoMp3.Properties.GetMusicPropertiesAsync();
                musicProperties.Title = mp3ViewerModel.Tittle;
                musicProperties.Artist = mp3ViewerModel.Performers;
                musicProperties.AlbumArtist = mp3ViewerModel.Performers;
                musicProperties.Album = mp3ViewerModel.Album;

                await musicProperties.SavePropertiesAsync();
                await pseudoMp3.Properties.SavePropertiesAsync();

                //When writing finish, copy info to chosen place
                await pseudoMp3.CopyAndReplaceAsync(mp3);
                
                await new MessageDialog($"Done. Save file to {mp3.Path}").ShowAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        private void setThumbnail(string mp3Path, string imgPath)
        {
            TagLib.File mp3 = TagLib.File.Create(mp3Path);

            TagLib.Picture picture = new TagLib.Picture(imgPath)
            {
                Type = TagLib.PictureType.FrontCover,
                Description = "Cover",
                MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg,
            };

            mp3.Tag.Pictures = new TagLib.Picture[] { picture };
            mp3.Save();
        }

        private async Task<StorageFile> ConvertToMp3(StorageFile src, StorageFile dest)
        {
            MediaEncodingProfile profile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);

            MediaTranscoder mediaTranscoder = new MediaTranscoder();
            mediaTranscoder.HardwareAccelerationEnabled = true;

            PrepareTranscodeResult prepareTranscode = await mediaTranscoder.PrepareFileTranscodeAsync(src, dest, profile);
            if (prepareTranscode.CanTranscode)
            {
                await prepareTranscode.TranscodeAsync();
            }

            return dest;
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

        public async Task InitAsync(FileInfo _mp4)
        {
            mp4Path = _mp4.FullName;
            mp4 = await StorageFile.GetFileFromPathAsync(mp4Path);


            mp3ViewerModel.Tittle = Path.GetFileNameWithoutExtension(_mp4.Name);

            MediaClip mediaClip = await MediaClip.CreateFromFileAsync(mp4);
            mp3ViewerModel.Duration = mediaClip.OriginalDuration.TotalSeconds;
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

                StorageFolder tmpFolder = App.TemporaryFolder;

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

        ~Mp3ViewerViewModel()
        {

        }
    }
}
