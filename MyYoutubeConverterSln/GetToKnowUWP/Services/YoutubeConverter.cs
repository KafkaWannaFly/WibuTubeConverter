using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using TagLib;
using VideoLibrary;
using Xabe.FFmpeg;

namespace Core.TryYoutubeApi
{
    public class YoutubeConverter
    {
        VideoClient clientServices;
        Client<YouTubeVideo> clientRequest;

        /// <summary>
        /// Files stored in this folder will be deleted when app is closed 
        /// </summary>
        public static string TemporaryFolder = "tmp/";
        public static string FFmpegPath = "FFmpeg binary";

        static string[] videoExtension = 
        {
            ".AVI", ".MP4", ".DIVX", ".WMV", ".FLV", ".3GP", ".WMV", ".WEBM"
        };
        
        public YoutubeConverter()
        {
            clientRequest = Client.For(YouTube.Default);
            clientServices = new VideoClient();

            FFmpeg.SetExecutablesPath(FFmpegPath);

            
        }

        ~YoutubeConverter()
        {
            clientRequest.Dispose();
            clientServices.Dispose();
        }

        public async Task<YouTubeVideo> GetVideoInfoAsync(string url)
        {
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
            {
                throw new UriFormatException("Bad URL!");
            }
            return await clientRequest.GetVideoAsync(url);
        }

        /// <summary>
        /// Download video from Youtube
        /// </summary>
        /// <param name="uri">URL of video</param>
        /// <param name="folderToSaveIn">'temp/' All files in this folder will be delete after close the app</param>
        /// <returns>Object that contain infomation about downloaded file</returns>
        public async Task<FileInfo> DownloadVideoAsync(string uri, string folderToSaveIn)
        {
            Uri tmpUri;
            if (!Uri.TryCreate(uri, UriKind.RelativeOrAbsolute, out tmpUri))
            {
                throw new UriFormatException("Bad URL!");
            }
            if(!Directory.Exists(folderToSaveIn))
            {
                Directory.CreateDirectory(Path.GetFullPath(folderToSaveIn));
            }

            //IEnumerable<YouTubeVideo> youTubeVideos = await clientRequest.GetAllVideosAsync(uri);
            //if (!youTubeVideos.Any())
            //{
            //    throw new Exception($"{nameof(DownloadVideo)}: There are no result");
            //}

            //YouTubeVideo youTubeVideo = youTubeVideos.First();
            //foreach (var video in youTubeVideos)
            //{
            //    if(video.Resolution > youTubeVideo.Resolution)
            //    {
            //        youTubeVideo = video;
            //    }
            //}

            //Get highest resolution possible
            //int highestResolution = (from vid in youTubeVideos select vid.Resolution).Max();
            //var youTubeVideo = youTubeVideos.FirstOrDefault(vid => vid.Resolution == highestResolution);

            YouTubeVideo youTubeVideo = await clientRequest.GetVideoAsync(uri);

            //await File.WriteAllBytesAsync(folderToSaveIn + youTubeVideo.FullName,
            //    youTubeVideo.GetBytesAsync().GetAwaiter().GetResult());

            //Because Stream is implemented IDisposable, we must call Dispose directly or indirectly
            using (Stream sourceStream = await youTubeVideo.StreamAsync())
            {
                using(Stream destinationStream = File.OpenWrite(folderToSaveIn + youTubeVideo.FullName))
                {
                    await sourceStream.CopyToAsync(destinationStream);
                }
            }

            //await File.WriteAllBytesAsync(folderToSaveIn + youTubeVideo.FullName, await youTubeVideo.GetBytesAsync());

            return new FileInfo(folderToSaveIn + youTubeVideo.FullName);
        }

        /// <summary>
        /// Download YouTube video with highest resolution possible but don't have sound
        /// This is a known problem of YouTube or VideoLibrary
        /// Alternative solution is download this video one more time (default resolution will have sound). Extract audio from default video and attach to this
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="folderToSaveIn"></param>
        /// <returns></returns>
        public async Task<FileInfo> DownloadHrVideoAsync(string uri, string folderToSaveIn)
        {
            Uri tmpUri;
            if (!Uri.TryCreate(uri, UriKind.RelativeOrAbsolute, out tmpUri))
            {
                throw new UriFormatException("Bad URL!");
            }

            IEnumerable<YouTubeVideo> youTubeVideos = await clientRequest.GetAllVideosAsync(uri);
            if (!youTubeVideos.Any())
            {
                throw new Exception($"{nameof(DownloadHrVideoAsync)}: There are no result");
            }

            int highestResolution = youTubeVideos.Max(vid => vid.Resolution);
            YouTubeVideo youTubeVideo = youTubeVideos.FirstOrDefault(vid => vid.Resolution == highestResolution);

            //Because Stream is implemented IDisposable, we must call Dispose directly or indirectly
            using (Stream sourceStream = await youTubeVideo.StreamAsync())
            {
                using (Stream destinationStream = File.OpenWrite(folderToSaveIn + youTubeVideo.FullName))
                {
                    await sourceStream.CopyToAsync(destinationStream);
                }
            }

            return new FileInfo(folderToSaveIn + youTubeVideo.FullName);
        }

        public async Task<FileInfo> ConvertVideoToMp3Async(string source, string dest)
        {
            if(!File.Exists(source))
            {
                throw new FileNotFoundException($"{nameof(ConvertVideoToMp3Async)}: Can't find {source}");
            }
            
            var conversion = await FFmpeg.Conversions.FromSnippet.ExtractAudio(source, dest);
            await conversion.Start();

            return new FileInfo(dest);
        }

        public async Task<FileInfo> ConvertVideoToMp3Async(FileInfo source, string dest)
        {
            return await this.ConvertVideoToMp3Async(source.FullName, dest);
        }

        public async Task<FileInfo> GetVideoSnapshotAsync(string source, double second)
        {
            FileInfo fileInfo = new FileInfo(source);
            if(!fileInfo.Exists)
            {
                throw new FileNotFoundException($"{nameof(this.GetVideoSnapshotAsync)}: Can't find {source}");
            }

            if(!videoExtension.Contains(fileInfo.Extension, StringComparer.OrdinalIgnoreCase))
            {
                throw new Exception($"{nameof(this.GetVideoSnapshotAsync)}: We expect a video extension, not {fileInfo.Extension}");
            }

            string outputImg = TemporaryFolder + Path.GetFileNameWithoutExtension(fileInfo.Name) + ".jpg";
            IConversion conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(fileInfo.FullName,
                                                            outputImg,
                                                            TimeSpan.FromSeconds(second));
            IConversionResult conversionResult = await conversion.Start();

            return new FileInfo(outputImg);
        }

        public async Task<FileInfo> SetMp3Thumbnail(string mp3Path, string picturePath)
        {
            if(!System.IO.File.Exists(mp3Path))
            {
                throw new FileNotFoundException($"{nameof(SetMp3Thumbnail)}: Can't find {mp3Path}");
            }

            TagLib.File mp3 = TagLib.File.Create(mp3Path);

            TagLib.Picture picture = new TagLib.Picture(picturePath)
            {
                Type = TagLib.PictureType.FrontCover,
                Description = "Cover",
                MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg,
            };
            
            mp3.Tag.Pictures = new TagLib.Picture[] { picture };
            mp3.Save();

            return new FileInfo(mp3.Name);
        }

        /// <summary>
        /// Set few detail infomation of mp3 file. Cover picture is untouched
        /// </summary>
        /// <param name="mp3Path"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public FileInfo SetMp3DetailInfo(string mp3Path, TagLib.Tag detail)
        {
            if (!System.IO.File.Exists(mp3Path))
            {
                throw new FileNotFoundException($"{nameof(SetMp3Thumbnail)}: Can't find {mp3Path}");
            }

            TagLib.File mp3 = TagLib.File.Create(mp3Path);
            mp3.Tag.Title = detail.Title;
            mp3.Tag.Performers = detail.Performers;
            mp3.Tag.Album = detail.Album;

            mp3.Save();

            return new FileInfo(mp3.Name);
        }
    }
}
