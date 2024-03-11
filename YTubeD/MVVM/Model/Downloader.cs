using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTubeD.Core;
using YoutubeExplode;

namespace YTubeD.MVVM.Model
{
    internal class Downloader : ObservableObject
    {
        private string _outputDirectory = string.Empty;

        public YoutubeVideo Video { get; set; }
        public string OutputDirectory 
        {
            get { return _outputDirectory; }
            set
            {
                _outputDirectory = value;
                OnPropertyChanged();
            }
        }

        public Downloader()
        {
            Video = new YoutubeVideo();
        }
    }
}
