using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using YTubeD.Core;
using YTubeD.MVVM.Models;
using YTubeD.Services;

namespace YTubeD.MVVM.ViewModels.Dialogs
{
    class SettingsViewModel : ObservableObject
    {
        private readonly SettingsModel Settings;
        private UpdateService UpdateServiceProperty;
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
        public StatusMessageService StatusMessage {  get; set; }

        public ICommand ChooseDirectoryCommand { get; }
        public ICommand SaveAndCloseCommand { get; }
        public ICommand CheckForUpdatesCommand { get; }

        public SettingsViewModel()
        {
            Settings = new SettingsModel();
            Settings.LoadSettings();
            UpdateServiceProperty = new UpdateService();
            SavingPath = Settings.SavingPath;
            StatusMessage = new StatusMessageService(Visibility.Hidden, "", 2000);
            ChooseDirectoryCommand = new RelayCommand(ChooseDirectory);
            SaveAndCloseCommand = new RelayCommand(SaveAndClose);
            CheckForUpdatesCommand = new RelayCommand(CheckForUpdates);
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
        private async void CheckForUpdates(object parameter)
        {
            try
            {
                var updateVersion = await UpdateServiceProperty.CheckForUpdatesAsync();
                if (updateVersion is null)
                {
                    StatusMessage.Visibility = Visibility.Visible;
                    StatusMessage.Message = "You are up to date";
                    await Task.Delay(StatusMessage.TimeDelay);
                    StatusMessage.Visibility = Visibility.Hidden;
                    return;
                }

                StatusMessage.Visibility = Visibility.Visible;
                StatusMessage.Message = $"Downloading update v{updateVersion}...";
                
                await UpdateServiceProperty.PrepareUpdateAsync(updateVersion);

                StatusMessage.Message = "Update has been downloaded and will be installed when you exit";
                UpdateServiceProperty.FinalizeUpdate(true);

                await Task.Delay(StatusMessage.TimeDelay);
                StatusMessage.Visibility = Visibility.Hidden;
            }
            catch
            {
                StatusMessage.Visibility = Visibility.Visible;
                StatusMessage.Message = "Failed to perform application update";
                await Task.Delay(StatusMessage.TimeDelay);
                StatusMessage.Visibility = Visibility.Hidden;
            }
        }
    }
}
