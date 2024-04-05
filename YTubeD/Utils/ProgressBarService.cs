using System.Windows;
using System.Windows.Markup;
using YTubeD.Core;

namespace YTubeD.Utils
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
        private double _progress;
        public double Progress
        {
            get => _progress;
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _progress = value;
                }
                else
                {
                    _progress = 0;
                }
                OnPropertyChanged();
            }
        }

        public ProgressBarService()
        {
            Visibility = Visibility.Hidden;
            IsIndeterminate = false;
            Progress = 0.0;
        }
        public ProgressBarService(Visibility visibility, bool isIndeterminate, double progress)
        {
            Visibility = visibility;
            IsIndeterminate = isIndeterminate;
            Progress = progress;
        }
    }
}
