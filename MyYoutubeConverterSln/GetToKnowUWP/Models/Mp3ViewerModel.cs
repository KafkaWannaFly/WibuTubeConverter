using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToKnowUWP.Models
{
    public class Mp3ViewerModel
    {
        public string Tittle 
        { 
            get => this.tittle; 
            set
            {
                this.tittle = value;
            }
        }
        string tittle = "";
        public string Performers 
        { 
            get => this.performers; 
            set 
            {
                this.performers = value;
            } 
        }
        string performers = "";
        public string Album 
        { 
            get=> this.album;
            set
            {
                this.album = value;
            }
        }
        string album = "";
        public double Snapshot 
        { 
            get => this.snapshot;
            set
            {
                this.snapshot = value;
            } 
        }
        double snapshot = 0f;
        public string ImagePath 
        { 
            get => this.imagePath;
            set
            {
                this.imagePath = value;
            } 
        }
        string imagePath = "/Assets/crow.png";
        public double Duration 
        { 
            get => this.duration;
            set
            {
                this.duration = value;
            } 
        }
        double duration = 0f;
        public Mp3ViewerModel()
        {

        }

    }
}
