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

namespace YTubeD.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        // Properties and fields
        Downloader YTDownloader { get; set; }       
        private bool _isUrlValid;
        private ObservableCollection<IStreamInfo> _qualities;
        private IStreamInfo _selectedQuality;
        private string _statusMessage;

        public bool IsUrlValid
        {
            get => _isUrlValid;
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
            get => YTDownloader.Video.Url;
            set
            {
                YTDownloader.Video.Url = value;
                OnPropertyChanged();
                ValidateUrlCommand.Execute(null);
            }
        }
        public string SavingPath
        {
            get => YTDownloader.OutputDirectory;
            set
            {
                YTDownloader.OutputDirectory = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<IStreamInfo> Qualities 
        {
            get => _qualities;
            set
            {
                _qualities = value;
                OnPropertyChanged();
            }
        }
        public IStreamInfo SelectedQuality
        {
            get => _selectedQuality;
            set
            {
                _selectedQuality = value;
                OnPropertyChanged();
            }
        }
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand ValidateUrlCommand { get; }
        public ICommand ChooseDirectoryCommand { get; }

        // Methods
        public MainViewModel()
        {
            YTDownloader = new Downloader();
            Qualities = new ObservableCollection<IStreamInfo>();
            ValidateUrlCommand = new RelayCommand(ValidateUrl);
            ChooseDirectoryCommand = new RelayCommand(ChooseDirectory);
        }

        private async void ValidateUrl(object parameter)
        {          
            IsUrlValid = await IsYoutubeUrlValid();
            if (IsUrlValid)
            {
                StatusMessage = "Fetching Qualities...";
                await FetchQualities();
                StatusMessage = "";
            }
            else
            {
                StatusMessage = "Invalid URL";
            }
        }

        private async Task<bool> IsYoutubeUrlValid()
        {
            return await YTDownloader.IsUrlValid();
        }

        private void ChooseDirectory(object parameter)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    SavingPath = dialog.SelectedPath;
                }
            }
        }

        private async Task FetchQualities()
        {
            var qualities = await YTDownloader.FetchQualities();

            Qualities.Clear();
            foreach (var quality in qualities)
            {
                Qualities.Add(quality);
            }
            SelectedQuality = Qualities.First();
        }
    }
}
