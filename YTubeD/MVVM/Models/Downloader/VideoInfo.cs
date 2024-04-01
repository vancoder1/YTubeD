using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTubeD.Core;

namespace YTubeD.MVVM.Models.Downloader
{
    internal class VideoInfo : ObservableObject
    {
        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        private string _author = string.Empty;
        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }
        private string _duration = string.Empty;
        public string Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }
    }
}
