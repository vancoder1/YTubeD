using System.Windows.Input;
using YTubeD.Core;
using YTubeD.MVVM.ViewModels.Components;
using YTubeD.MVVM.Views.Dialogs;
using YTubeD.Utils;

namespace YTubeD.MVVM.ViewModels
{
    class MainViewModel : ObservableObject
    {
        public VideoDownloaderViewModel VideoDownloaderVM { get; set; }

        private object _currentView = null!;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
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

        public ICommand OpenSettingsCommand { get; }
        public ICommand SubmitUrlCommand { get; }

        public MainViewModel()
        {
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
            await Task.Run(() => UpdateUrl(Url));
        }
    }
}
