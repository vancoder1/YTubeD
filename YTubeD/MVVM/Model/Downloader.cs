using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTubeD.Core;

namespace YTubeD.MVVM.Model
{
    internal class Downloader : ObservableObject
    {
        public YoutubeVideo Video { get; set; }
        public string OutputDirectory { get; set; }

        public Downloader()
        {
            Video = new YoutubeVideo();
            OutputDirectory = "";
        }
    }
}
