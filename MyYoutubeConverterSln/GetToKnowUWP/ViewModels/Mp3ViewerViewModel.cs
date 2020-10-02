using Core.TryYoutubeApi;
using GetToKnowUWP.Models;
using GetToKnowUWP.ViewModels.Commands;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;


namespace GetToKnowUWP.ViewModels
{
    public class Mp3ViewerViewModel: INotifyPropertyChanged
    {
        public Mp3ViewerModel mp3ViewerModel = new Mp3ViewerModel();
        YoutubeConverter youtubeConverter = new YoutubeConverter();
        FileInfo mp4;
        TagLib.File mediaDetail;

        public Mp3ViewerViewModel()
        {
            
        }

        //public Mp3ViewerViewModel(FileInfo _mp4)
        //{
        //    this.mp4 = _mp4;
        //    this.mediaDetail = TagLib.File.Create(mp4.FullName);

        //    this.mp3ViewerModel.Tittle = this.mp4.Name;
        //    this.mp3ViewerModel.Duration = this.mediaDetail.Properties.Duration.TotalSeconds;
        //}

        public void Init(FileInfo _mp4)
        {
            this.mp4 = _mp4;
            this.mediaDetail = TagLib.File.Create(mp4.FullName);

            this.mp3ViewerModel.Tittle = Path.GetFileNameWithoutExtension(this.mp4.Name);
            this.mp3ViewerModel.Duration = this.mediaDetail.Properties.Duration.TotalSeconds;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
