using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTubeD.Core;
using YoutubeExplode;

namespace YTubeD.MVVM.Model
{
    internal class YoutubeVideo : ObservableObject
    {
        private string _url = string.Empty;
        private string _title = string.Empty;
        private string _author = string.Empty;
        private string _duration = string.Empty;

        public string Url 
        { 
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }
        public string Title { 
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string Author 
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }
        public string Duration { 
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }
        
        public YoutubeVideo()
        { 
        
        }
    }
}
