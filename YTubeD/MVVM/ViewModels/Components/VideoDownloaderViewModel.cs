using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using YoutubeExplode.Videos.Streams;
using YTubeD.Core;
using YTubeD.MVVM.Model;
using YTubeD.MVVM.Models.Downloader;

namespace YTubeD.MVVM.ViewModels.Components
{
    class VideoDownloaderViewModel : ObservableObject
    {
        public Downloader YTDownloader { get; set; }
        private string _url = string.Empty;
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<VideoInfo> _videos;
        public ObservableCollection<VideoInfo> Videos
        {
            get => _videos;
            set
            {
                _videos = value;
                OnPropertyChanged();
            }
        }
        private VideoInfo _selectedVideo;
        public VideoInfo SelectedVideo
        {
            get => _selectedVideo;
            set
            {
                _selectedVideo = value;
                OnPropertyChanged();
            }
        }
        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand DownloadCommand { get; }
        public ICommand ClearAllCommand { get; }

        public VideoDownloaderViewModel()
        {
            YTDownloader = new Downloader();
            DownloadCommand = new RelayCommand(Download);
            ClearAllCommand = new RelayCommand(ClearAll);
        }       

        private async void Download(object parameter)
        {
            StatusMessage = "Downloading...";
            try
            {
                //await YTDownloader.Download(Url);
            }
            catch (ArgumentNullException e)
            {
                StatusMessage = e.Message;
            }
            StatusMessage = "Download Completed!";
        }
        private void ClearAll(object parameter)
        {
            Videos.Clear();
        }
    }
}
