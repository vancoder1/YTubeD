using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using YTubeD.Core;
using YTubeD.MVVM.Models;

namespace YTubeD.MVVM.ViewModels.Dialogs
{
    class SettingsViewModel : ObservableObject
    {
        private readonly SettingsModel Settings;
        private string _savingPath = string.Empty;
        public string SavingPath
        {
            get => _savingPath;
            set
            {
                _savingPath = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChooseDirectoryCommand { get; }
        public ICommand SaveAndCloseCommand { get; }

        public SettingsViewModel()
        {
            Settings = new SettingsModel();
            Settings.LoadSettings();
            SavingPath = Settings.SavingPath;
            ChooseDirectoryCommand = new RelayCommand(ChooseDirectory);
            SaveAndCloseCommand = new RelayCommand(SaveAndClose);
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
        private void SaveAndClose(object parameter)
        {
            Settings.SavingPath = SavingPath;
            Settings.SaveSettings();
            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}
