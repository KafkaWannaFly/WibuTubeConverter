using VideoLibrary;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Threading;
using System.Diagnostics;

namespace Core.TryYoutubeApi
{
    class Program
    {
        static void Main(string[] args)
        {
            //var source = @"tmp/";
            //var youtube = YouTube.Default;
            //var vid = youtube.GetVideo("https://www.youtube.com/watch?v=OC1jwn0Hv70&list=PLHB7pQtzGtiXw_toturl3vfPw0KJwIfdd");

            //File.WriteAllBytes(source + vid.FullName, vid.GetBytes());

            //var inputFile = new MediaFile { Filename = source + vid.FullName };
            //var outputFile = new MediaFile { Filename = $"{source + vid.FullName}.mp3" };

            //using (var engine = new Engine())
            //{
            //    engine.GetMetadata(inputFile);

            //    engine.Convert(inputFile, outputFile);
            //}

            try
            {
                //new Program().ConvertYoutuToMp3("https://www.youtube.com/watch?v=B6Zsr7m1GFI", "").Wait();
                //new Program().GetVideoInfo(@"https://www.youtube.com/watch?v=S12-oHtTMwk").Wait();

                YoutubeConverter youtubeConverter = new YoutubeConverter();

                //long normalRs, highRs;
                //var stopWatch = new Stopwatch();
                //stopWatch.Start();
                FileInfo mp4 = youtubeConverter.DownloadVideoAsync(@"https://www.youtube.com/watch?v=d10_sLHZNhA&list=PLFJRIUbt-ELjQZqVbzvCGerxXgjGp_Ww5&index=21",
                                                                    @"tmp/").GetAwaiter().GetResult();
                //stopWatch.Stop();
                //normalRs = stopWatch.ElapsedMilliseconds;

                //stopWatch.Start();
                //youtubeConverter.DownloadHrVideoAsync("https://www.youtube.com/watch?v=_z411ShLWHI&list=PLFJRIUbt-ELjQZqVbzvCGerxXgjGp_Ww5&index=13&t=0s",
                //    YoutubeConverter.TemporaryFolder).Wait();
                //stopWatch.Stop();
                //highRs = stopWatch.ElapsedMilliseconds;

                //Console.WriteLine($"Normal: {normalRs}\nHigh: {highRs}");

                //FileInfo mp4 = new FileInfo(YoutubeConverter.TemporaryFolder + "Ilya Kuznetsov - Sight.mp4");

                //FileInfo mp4 = youtubeConverter.DownloadVideoAsync(@"https://www.youtube.com/watch?v=-HlQld80SkE&list=PLFJRIUbt-ELjQZqVbzvCGerxXgjGp_Ww5&index=6",
                //                                                    @"tmp/").GetAwaiter().GetResult();

                FileInfo mp3 = youtubeConverter.ConvertVideoToMp3Async(mp4.FullName,
                       Path.ChangeExtension(mp4.FullName, "mp3")).GetAwaiter().GetResult();

                FileInfo picture = youtubeConverter.GetVideoSnapshotAsync(mp4.FullName, 20).GetAwaiter().GetResult();

                youtubeConverter.SetMp3Thumbnail(mp3.FullName, picture.FullName).Wait();

                Console.WriteLine($"Save at {mp3.FullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
