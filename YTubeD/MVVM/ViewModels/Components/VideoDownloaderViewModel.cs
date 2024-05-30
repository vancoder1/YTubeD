using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using YTubeD.Core;
using YTubeD.MVVM.Model;
using YTubeD.MVVM.Models.Downloader;
using YTubeD.Services;
using YTubeD.Utils;

namespace YTubeD.MVVM.ViewModels.Components
{
    class VideoDownloaderViewModel : ObservableObject
    {
        public Downloader YTDownloader { get; set; }
        private VideoInfoFetcher Fetcher { get; set; }
        public ProgressBarService ProgressBar { get; set; }
        public StatusMessageService StatusMessage { get; set; }

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
            ProgressBar = new ProgressBarService(Visibility.Hidden, true);
            StatusMessage = new StatusMessageService(Visibility.Hidden, "", 2000);
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
            ProgressBar.Visibility = Visibility.Visible;
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
            ProgressBar.Visibility = Visibility.Hidden;
        }

        private async void Download(object parameter)
        {
            ProgressBar.Visibility = Visibility.Visible;
            try
            {
                foreach (var video in Videos)
                {
                    await YTDownloader.Download(video, SelectedOption);
                }
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
            ProgressBar.Visibility = Visibility.Hidden;
            StatusMessage.Message = "Download completed";
            StatusMessage.Visibility = Visibility.Visible;
            await Task.Delay(StatusMessage.TimeDelay);
            StatusMessage.Visibility = Visibility.Hidden;
        }

        private async void ClearAll(object parameter)
        {
            Videos.Clear();
            ProgressBar.Visibility = Visibility.Hidden;
            StatusMessage.Message = "Video queue cleared";
            StatusMessage.Visibility = Visibility.Visible;
            await Task.Delay(StatusMessage.TimeDelay);
            StatusMessage.Visibility = Visibility.Hidden;
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
