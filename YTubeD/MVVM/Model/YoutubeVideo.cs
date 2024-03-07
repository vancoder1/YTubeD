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
        public string Url { get; set; }
        public string VideoName { get; set; }
        
        public YoutubeVideo()
        {
            Url = "";
            VideoName = "";
        }
    }
}
