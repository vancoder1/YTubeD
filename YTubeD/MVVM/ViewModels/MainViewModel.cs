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

namespace YTubeD.MVVM.ViewModels
{
    class MainViewModel : ObservableObject
    {
        // Properties and fields
        public EmptyDownloaderViewModel EmptyDownloaderVM { get; set; }
        public VideoDownloaderViewModel VideoDownloaderVM { get; set; }

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

        // Commands
        public ICommand OpenSettingsCommand { get; }

        // Constructor
        public MainViewModel()
        {
            EmptyDownloaderVM = new EmptyDownloaderViewModel();
            VideoDownloaderVM = new VideoDownloaderViewModel();
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            CurrentView = VideoDownloaderVM;
        }

        // Methods
        private void OpenSettings(object parameter)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.DataContext = new SettingsViewModel();
            settingsWindow.ShowDialog();
        }
    }
}
