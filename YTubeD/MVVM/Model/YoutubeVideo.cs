using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTubeD.Core;

namespace YTubeD.MVVM.Model
{
    internal class YoutubeVideo : ObservableObject
    {
        public string Url 
        { 
            get { return Url; }
            set
            {
                Url = value;
                OnPropertyChanged();
            }
        }
        public string Title { 
            get { return Title; }
            set
            {
                Title = value;
                OnPropertyChanged();
            }
        }
        public string Author 
        {
            get { return Author; }
            set
            {
                Author = value;
                OnPropertyChanged();
            }
        }
        public string Duration { 
            get { return Duration; }
            set
            {
                Duration = value;
                OnPropertyChanged();
            }
        }
        
        public YoutubeVideo()
        {
            Url = "";
            Title = "";
            Author = "";
            Duration = "";
        }
    }
}
