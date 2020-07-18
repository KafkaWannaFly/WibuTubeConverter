using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
