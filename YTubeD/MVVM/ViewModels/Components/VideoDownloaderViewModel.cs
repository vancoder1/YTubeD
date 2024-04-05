using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using YTubeD.Core;
using YTubeD.MVVM.Model;
using YTubeD.MVVM.Models.Downloader;
using YTubeD.Utils;

namespace YTubeD.MVVM.ViewModels.Components
{
    class VideoDownloaderViewModel : ObservableObject
    {
        public Downloader YTDownloader { get; set; }
        private VideoInfoFetcher Fetcher { get; set; }
        public ProgressBarService FetchingProgressBar { get; set; }
        public ProgressBarService DownloadingProgressBar { get; set; }

        public ObservableCollection<VideoInfo> Videos { get; set; }
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
        public ObservableCollection<DownloadOption> DownloadOptions { get; set; }
        private DownloadOption _selectedOption = null!;
        public DownloadOption SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged();
            }
        }

        public ICommand DownloadCommand { get; }
        public ICommand ClearAllCommand { get; }
        public ICommand RemoveElementCommand { get; }

        public VideoDownloaderViewModel()
        {
            YTDownloader = new Downloader();
            Fetcher = new VideoInfoFetcher();
            FetchingProgressBar = new ProgressBarService(Visibility.Hidden, true);
            DownloadingProgressBar = new ProgressBarService(Visibility.Hidden, true);
            Videos = new ObservableCollection<VideoInfo>();
            DownloadOptions = new ObservableCollection<DownloadOption>
            {
                new(DownloadPreference.UpTo144p),
                new(DownloadPreference.UpTo240p),
                new(DownloadPreference.UpTo360p),
                new(DownloadPreference.UpTo480p),
                new(DownloadPreference.UpTo720p),
                new(DownloadPreference.UpTo1080p),
                new(DownloadPreference.UpTo1440p),
                new(DownloadPreference.UpTo2160p),
                new(DownloadPreference.AudioMp3),
            };
            SelectedOption = DownloadOptions[5];
            DownloadCommand = new RelayCommand(Download);
            ClearAllCommand = new RelayCommand(ClearAll);
            RemoveElementCommand = new RelayCommand(RemoveElement);
            EventAggregatorUtility.EventAggregator.GetEvent<UpdateUrlEvent>().Subscribe(UpdateUrl);
        }

        private async void UpdateUrl(string url)
        {
            FetchingProgressBar.Visibility = Visibility.Visible;
            if (url != null)
            {
                if (await Fetcher.IsUrlValid(url))
                {
                    VideoInfoFetcher infoFetcher = new();
                    VideoInfo info = await infoFetcher.GetVideoInfoAsync(url);
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Videos.Add(info);
                    });
                }
            }
            FetchingProgressBar.Visibility = Visibility.Hidden;
        }

        private async void Download(object parameter)
        {
            DownloadingProgressBar.Visibility = Visibility.Visible;
            try
            {
                foreach (var video in Videos)
                {
                    await YTDownloader.Download(video, SelectedOption);
                }
            }
            catch (ArgumentNullException e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
            DownloadingProgressBar.Visibility = Visibility.Hidden;
        }

        private void ClearAll(object parameter)
        {
            Videos.Clear();
        }

        private void RemoveElement(object parameter)
        {
            if (parameter != null && parameter is VideoInfo videoInfo)
            {
                Videos.Remove(videoInfo);
            }
        }
    }
}
