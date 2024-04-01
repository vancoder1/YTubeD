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

namespace YTubeD.MVVM.ViewModels
{
    class MainViewModel : ObservableObject
    {
        public VideoDownloaderViewModel VideoDownloaderVM { get; set; }

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

        public ICommand OpenSettingsCommand { get; }
        public ICommand SubmitUrlCommand { get; }

        public MainViewModel()
        {
            VideoDownloaderVM = new VideoDownloaderViewModel();
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            SubmitUrlCommand = new RelayCommand(SubmitUrl);
            CurrentView = VideoDownloaderVM;
        }

        private void OpenSettings(object parameter)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
        private void SubmitUrl(object parameter)
        {
            VideoDownloaderVM.YTDownloader.Url = Url;
        }
    }
}
