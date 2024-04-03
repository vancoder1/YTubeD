using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using YTubeD.Core;
using YTubeD.MVVM.Model;
using System.Windows;
using YoutubeExplode.Videos.Streams;
using System.Formats.Asn1;
using YTubeD.MVVM.Views.Dialogs;
using YTubeD.MVVM.ViewModels.Components;
using YTubeD.MVVM.Models.Downloader;
using YTubeD.Utils;

namespace YTubeD.MVVM.ViewModels
{
    class MainViewModel : ObservableObject
    {
        public VideoDownloaderViewModel VideoDownloaderVM { get; set; }

        private Downloader YTDownloader { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenSettingsCommand { get; }
        public ICommand SubmitUrlCommand { get; }

        public MainViewModel()
        {           
            YTDownloader = new Downloader();
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            SubmitUrlCommand = new RelayCommand(SubmitUrl);
            VideoDownloaderVM = new VideoDownloaderViewModel();
        }

        public void UpdateUrl(string url)
        {
            EventAggregatorUtility.EventAggregator.GetEvent<UpdateUrlEvent>().Publish(url);
        }

        private void OpenSettings(object parameter)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private async void SubmitUrl(object parameter)
        {
            if (Url != null)
            {
                if (await YTDownloader.IsUrlValid(Url))
                {
                    //VideoInfoFetcher infoFetcher = new VideoInfoFetcher();
                    //VideoInfo info = await infoFetcher.GetVideoInfoAsync(Url);
                    //VideoDownloaderVM.Videos.Add(info);
                    UpdateUrl(Url);
                }
            }
        }
    }
}
