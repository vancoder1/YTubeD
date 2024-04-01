using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using YTubeD.Core;
using YTubeD.MVVM.Models;

namespace YTubeD.MVVM.ViewModels.Dialogs
{
    class SettingsViewModel : ObservableObject
    {
        private SettingsModel Settings;
        public ICommand ChooseDirectoryCommand { get; }

        public SettingsViewModel() 
        {
            Settings = new SettingsModel();
            Settings.LoadSettings();
            ChooseDirectoryCommand = new RelayCommand(ChooseDirectory);
        }

        private void ChooseDirectory(object parameter)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Settings.SavingPath = dialog.SelectedPath;
                }
            }
        }
    }
}
