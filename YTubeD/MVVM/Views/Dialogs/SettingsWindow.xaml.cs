using System.Windows;
using YTubeD.MVVM.ViewModels.Dialogs;

namespace YTubeD.MVVM.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            this.DataContext = new SettingsViewModel();
            this.Owner = Application.Current.MainWindow;
        }
    }
}
