using GetToKnowUWP.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace GetToKnowUWP.Models
{
    public class Mp3ViewerModel: INotifyPropertyChanged
    {
        public string Tittle 
        { 
            get => this.tittle; 
            set
            {
                this.tittle = value;
                this.OnPropertyChanged();
            }
        }
        string tittle = "";
        public string Performers 
        { 
            get => this.performers; 
            set 
            {
                this.performers = value;
                this.OnPropertyChanged();
            } 
        }
        string performers = "";
        public string Album 
        { 
            get=> this.album;
            set
            {
                this.album = value;
                this.OnPropertyChanged();
            }
        }
        string album = "";

        bool useSnapshot = false;
        public bool UseSnapshot
        {
            get => useSnapshot;
            set
            {
                useSnapshot = value;
                this.OnPropertyChanged();
            }
        }

        public CommandEventHandler<bool> UseSnapshotCommand
        {
            get
            {
                return new CommandEventHandler<bool>((isCheck) =>
                {
                    UseSnapshot = isCheck;

                    this.UseDefaultImage(isCheck);
                });
            }
        }
        public double Snapshot 
        { 
            get => this.snapshot;
            set
            {
                this.snapshot = value;
                this.OnPropertyChanged();
            } 
        }
        double snapshot = 0f;
        public string ImagePath 
        {
            get
            {
                return currentImgPath;
            }
            set
            {
                currentImgPath = value;
                this.OnPropertyChanged();
            }
        }
        string defaultImg = "/Assets/default_thumbnail.jpg";
        string currentImgPath = "/Assets/crow.png";
        string mp3ImgPath = "/Assets/default_thumbnail.jpg";
        public double Duration 
        { 
            get => this.duration;
            set
            {
                this.duration = value;
                this.OnPropertyChanged();
            } 
        }
        double duration = 0f;


        public Mp3ViewerModel()
        {

        }

        public void UseDefaultImage(bool isUse)
        {
            if (isUse)
            {
                ImagePath = "/Assets/default_thumbnail.jpg";
            }
            else
            {
                ImagePath = "/Assets/crow.png";
            }
        }

        #region Implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
