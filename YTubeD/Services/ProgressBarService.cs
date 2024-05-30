using System.Windows;
using YTubeD.Core;

namespace YTubeD.Services
{
    public class ProgressBarService : ObservableObject
    {
        private Visibility _visibility;
        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }
        private bool _isIndeterminate;
        public bool IsIndeterminate
        {
            get => _isIndeterminate;
            set
            {
                _isIndeterminate = value;
                OnPropertyChanged();
            }
        }

        public ProgressBarService()
        {
            Visibility = Visibility.Hidden;
            IsIndeterminate = false;
        }

        public ProgressBarService(Visibility visibility, bool isIndeterminate)
        {
            Visibility = visibility;
            IsIndeterminate = isIndeterminate;
        }
    }
}
