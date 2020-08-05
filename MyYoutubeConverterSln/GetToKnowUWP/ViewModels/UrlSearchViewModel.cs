using Core.TryYoutubeApi;
using GetToKnowUWP.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
                return await youtubeConverter.DownloadVideoAsync(url, YoutubeConverter.TemporaryFolder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
