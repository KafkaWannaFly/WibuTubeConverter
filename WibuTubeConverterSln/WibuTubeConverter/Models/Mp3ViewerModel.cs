using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WibuTubeConverter.ViewModels.Commands;
using Windows.ApplicationModel;
using Windows.Storage;

namespace WibuTubeConverter.Models
{
    public class Mp3ViewerModel : INotifyPropertyChanged
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

        private string tittle = "";

        public string Performers
        {
            get => this.performers;
            set
            {
                this.performers = value;
                this.OnPropertyChanged();
            }
        }

        private string performers = "";

        public string Album
        {
            get => this.album;
            set
            {
                this.album = value;
                this.OnPropertyChanged();
            }
        }

        private string album = "";

        public bool UseSnapshot
        {
            get => useSnapshot;
            set
            {
                useSnapshot = value;
                this.OnPropertyChanged();
            }
        }

        private bool useSnapshot = false;

        public CommandEventHandler<bool> UseSnapshotCommand
        {
            get
            {
                return new CommandEventHandler<bool>((isCheck) =>
                {
                    UseVidSnapshot(isCheck);
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

        private double snapshot = 0f;

        private string imagePath = "/Assets/total-black.png";
        private string defaultImg = Package.Current.InstalledLocation.Path + "/Assets/default_thumbnail.jpg";

        public string ImagePath
        {
            get
            {
                if (UseSnapshot)
                    return imagePath;
                else
                    return defaultImg;
            }
            set
            {
                imagePath = value;
                this.OnPropertyChanged();
            }
        }

        private TimeSpan beginTime = TimeSpan.FromSeconds(0);

        public string BeginTime
        {
            get
            {
                return beginTime.ToString();
            }
            set
            {
                try
                {
                    TimeSpan time;
                    bool canParse = TimeSpan.TryParse(value, out time);
                    if (!canParse)
                    {
                        throw new FormatException($"{BeginTime}: Can't parse value!");
                    }
                    if(time.Ticks < 0)
                    {
                        throw new ArgumentException();
                    }

                    setBeginTime(time);
                }
                catch (FormatException)
                {
                    BeginTime = beginTime.ToString();
                }
                catch(ArgumentException)
                {
                    BeginTime = beginTime.ToString();
                }
                catch (Exception)
                {
                    BeginTime = beginTime.ToString();
                }
            }
        }

        private TimeSpan endTime;

        public string EndTime
        {
            get
            {
                return endTime.ToString();
            }
            set
            {
                try
                {
                    TimeSpan time;
                    bool canParse = TimeSpan.TryParse(value, out time);
                    if (!canParse)
                    {
                        throw new FormatException($"{EndTime}: Can't parse value!");
                    }
                    if (time.Ticks < 0)
                    {
                        throw new ArgumentException();
                    }
                    setEndTime(time);
                }
                catch (FormatException)
                {
                    EndTime = endTime.ToString();
                }
                catch(ArgumentException)
                {
                    EndTime = endTime.ToString();
                }
                catch (Exception)
                {
                    EndTime = endTime.ToString();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Throw if time is larger than endTime
        /// </exception>
        /// <param name="time"></param>
        public void setBeginTime(TimeSpan time)
        {
            if(time > endTime)
            {
                throw new ArgumentException($"{nameof(setBeginTime)}: Begin time can't larger than end time!");
            }

            beginTime = time;
            OnPropertyChanged(nameof(BeginTime));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Throw if time is smaller than BeginTime
        /// </exception>
        /// <param name="time"></param>
        public void setEndTime(TimeSpan time)
        {
            if(time < beginTime)
            {
                throw new ArgumentException($"{nameof(setEndTime)}: End time can't smaller than begin time!");
            }

            endTime = time;
            OnPropertyChanged(nameof(EndTime));
        }
        public TimeSpan getBeginTime()
        {
            return beginTime;
        }
        public TimeSpan getEndTime()
        {
            return endTime;
        }

        public double Duration
        {
            get => this.duration;
            set
            {
                this.duration = value;
                this.OnPropertyChanged();
            }
        }

        private double duration = 0f;

        public Mp3ViewerModel()
        {
        }

        public void UseVidSnapshot(bool isUse)
        {
            UseSnapshot = isUse;

            OnPropertyChanged(nameof(ImagePath));
            //OnPropertyChanged(nameof(ThumbnailImage));
        }

        #region Implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion Implement INotifyPropertyChanged
    }
}