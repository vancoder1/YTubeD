using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTubeD.Core;
using YTubeD.MVVM.Model;

namespace YTubeD.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        Downloader YTDownloader { get; set; }
        public string Url
        {
            get { return YTDownloader.Video.Url; }
            set
            {
                YTDownloader.Video.Url = value;
                OnPropertyChanged();
            }
        }
        public string SavingPath
        {
            get { return YTDownloader.OutputDirectory; }
            set
            {
                YTDownloader.OutputDirectory = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            YTDownloader = new Downloader();
        }
    }
}
