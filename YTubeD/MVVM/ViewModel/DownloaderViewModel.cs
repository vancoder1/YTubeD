using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTubeD.MVVM.Model;
using YTubeD.Core;

namespace YTubeD.MVVM.ViewModel
{
    internal class DownloaderViewModel : ObservableObject
    {
        Downloader YTDownloader { get; set; }
        public ObservableCollection<string> Qualities { get; set; }
        public string Url
        {
            get { return Url; }
            set
            {
                YTDownloader.Video.Url = value;
                OnPropertyChanged();
            }
        }
        public string SelectedQuality
        {
            get { return SelectedQuality; }
            set
            {
                SelectedQuality = value;
                OnPropertyChanged();
            }
        }

        public DownloaderViewModel()
        {
            YTDownloader = new Downloader();
        }
    }
}
