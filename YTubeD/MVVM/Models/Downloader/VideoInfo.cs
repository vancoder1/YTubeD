using YoutubeExplode;
using YoutubeExplode.Videos;
using YTubeD.Core;

namespace YTubeD.MVVM.Models.Downloader
{
    internal class VideoInfo : ObservableObject
    {
        private string _url = string.Empty;
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }
        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        private string _author = string.Empty;
        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }
        private string? _duration = string.Empty;
        public string? Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }
        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
    }

    internal class VideoInfoFetcher
    {
        private readonly YoutubeClient _youtubeClient;

        public VideoInfoFetcher()
        {
            _youtubeClient = new YoutubeClient();
        }
        public VideoInfoFetcher(YoutubeClient youtubeClient)
        {
            _youtubeClient = youtubeClient;
        }

        public async Task<VideoInfo> GetVideoInfoAsync(string Url)
        {
            try
            {
                Video video = await _youtubeClient.Videos.GetAsync(Url);

                VideoInfo videoInfo = new VideoInfo
                {
                    Url = Url,
                    Title = video.Title,
                    Author = video.Author.ToString(),
                    Duration = video.Duration.ToString(),
                    Description = video.Description.ToString(),
                };

                return videoInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching video info: {ex.Message}");
                return null!;
            }
        }
    }
}
