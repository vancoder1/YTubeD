using System.Windows.Controls;
using YTubeD.MVVM.ViewModels.Components;

namespace YTubeD.MVVM.Views.Components
{
    /// <summary>
    /// Interaction logic for VideoDownloaderView.xaml
    /// </summary>
    public partial class VideoDownloaderView : UserControl
    {
        public VideoDownloaderView()
        {
            InitializeComponent();
            this.DataContext = new VideoDownloaderViewModel();
        }
    }
}
