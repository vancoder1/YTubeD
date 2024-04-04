using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;
using YoutubeExplode.Videos.Streams;
using YTubeD.Core;
using YTubeD.Utils;
using YTubeD.MVVM.Model;
using YTubeD.MVVM.Models.Downloader;
using YTubeD.Properties;
using YTubeD.MVVM.Models;

namespace YTubeD.MVVM.ViewModels.Components
{
    class VideoDownloaderViewModel : ObservableObject
    {
        public Downloader YTDownloader { get; set; }
        public ObservableCollection<VideoInfo> Videos
        {
            get; set;
        }
        private VideoInfo _selectedVideo = new VideoInfo();
        public VideoInfo SelectedVideo
        {
            get => _selectedVideo;
            set
            {
                _selectedVideo = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<DownloadOption> _downloadOptions;
        public ObservableCollection<DownloadOption> DownloadOptions
        {
            get => _downloadOptions;
            set
            {
                _downloadOptions = value;
                OnPropertyChanged();
            }
        }
        private DownloadOption _selectedOption;
        public DownloadOption SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged();
            }
        }
        private string _statusMessage = string.Empty;
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
        public ICommand RemoveElementCommand { get; }

        public VideoDownloaderViewModel()
        {
            YTDownloader = new Downloader();
            Videos = new ObservableCollection<VideoInfo>();
            DownloadOptions = new ObservableCollection<DownloadOption>()
            {
                new DownloadOption(DownloadPreference.UpTo144p),
                new DownloadOption(DownloadPreference.UpTo240p),
                new DownloadOption(DownloadPreference.UpTo360p),
                new DownloadOption(DownloadPreference.UpTo480p),
                new DownloadOption(DownloadPreference.UpTo720p),
                new DownloadOption(DownloadPreference.UpTo1080p),
                new DownloadOption(DownloadPreference.UpTo1440p),
                new DownloadOption(DownloadPreference.UpTo2160p),
                new DownloadOption(DownloadPreference.AudioMp3),
            };
            SelectedOption = DownloadOptions.Where(p => p.Preference == DownloadPreference.UpTo1080p).First();
            DownloadCommand = new RelayCommand(Download);
            ClearAllCommand = new RelayCommand(ClearAll);
            RemoveElementCommand = new RelayCommand(RemoveElement);
            EventAggregatorUtility.EventAggregator.GetEvent<UpdateUrlEvent>().Subscribe(UpdateUrl);
        }

        private async void UpdateUrl(string url)
        {
            VideoInfoFetcher infoFetcher = new VideoInfoFetcher();
            VideoInfo info = await infoFetcher.GetVideoInfoAsync(url);
            Videos.Add(info);
        }

        private async void Download(object parameter)
        {
            StatusMessage = "Downloading...";
            try
            {
                foreach (var video in Videos)
                {
                    await YTDownloader.Download(video, SelectedOption);
                }
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

        private void RemoveElement(object parameter)
        {

        }
    }
}
