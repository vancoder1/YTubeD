using System.Windows;
using YTubeD.Core;

namespace YTubeD.Services
{
    public class StatusMessageService : ObservableObject
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

        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public int TimeDelay { get; set; }

        public StatusMessageService()
        {
            Visibility = Visibility.Hidden;
            Message = string.Empty;
        }

        public StatusMessageService(Visibility visibility, string message, int timeDelay)
        {
            Visibility = visibility;
            Message = message;
            TimeDelay = timeDelay;
        }
    }
}
