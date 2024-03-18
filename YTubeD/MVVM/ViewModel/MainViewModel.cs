using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YTubeD.Core;
using YTubeD.MVVM.Model;

namespace YTubeD.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        Downloader YTDownloader { get; set; }
        private bool _isUrlValid;
        public bool IsUrlValid
        {
            get { return _isUrlValid; }
            set
            {
                if (_isUrlValid != value)
                {
                    _isUrlValid = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Url
        {
            get { return YTDownloader.Video.Url; }
            set
            {
                YTDownloader.Video.Url = value;
                OnPropertyChanged();
                ValidateUrlCommand.Execute(null);
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
        public ICommand ValidateUrlCommand { get; }

        public MainViewModel()
        {
            YTDownloader = new Downloader();
            ValidateUrlCommand = new RelayCommand(ValidateUrl);
        }
        private async void ValidateUrl(object parameter)
        {
            IsUrlValid = await IsYoutubeUrlValid();
        }
        private async Task<bool> IsYoutubeUrlValid()
        {
            return await YTDownloader.IsUrlValid();
        }
    }
}
