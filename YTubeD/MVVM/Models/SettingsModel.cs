using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTubeD.Core;

namespace YTubeD.MVVM.Models
{
    class SettingsModel : ObservableObject
    {
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

        public void LoadSettings()
        {
            SavingPath = Properties.Settings.Default.SavingPath;
        }
        public void SaveSettings()
        {
            
        }
    }
}
