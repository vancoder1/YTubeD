using YTubeD.Core;

namespace YTubeD.MVVM.Models
{
    class SettingsModel : ObservableObject
    {
        public string SavingPath { get; set; } = string.Empty;

        public void LoadSettings()
        {
            SavingPath = Properties.Settings.Default.SavingPath;
            if (SavingPath == null ||
                SavingPath == string.Empty ||
                SavingPath == "")
            {
                SavingPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";
            }
        }
        public void SaveSettings()
        {
            Properties.Settings.Default.SavingPath = SavingPath;
            Properties.Settings.Default.Save();
        }
    }
}
