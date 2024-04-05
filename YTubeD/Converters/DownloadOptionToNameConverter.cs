using System.Globalization;
using System.Windows.Data;
using YTubeD.MVVM.Models.Downloader;

namespace YTubeD.Converters
{
    [ValueConversion(typeof(DownloadOption), typeof(string))]
    public class DownloadOptionToNameConverter : IValueConverter
    {
        public static DownloadOptionToNameConverter Instance { get; } = new();
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DownloadOption downloadOption)
            {
                return DownloadPreferenceName.GetName(downloadOption.Preference);
            }
            return default(string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
