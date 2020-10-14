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
                youtubeConverter.GetVideoSnapshotAsync("tmp/Emotional Music_ _Love Story_ by Bob Bradley & Chris Egan & Adam Dennis.mp4", 10).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
