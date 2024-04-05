using YoutubeExplode;
using YoutubeExplode.Exceptions;
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

                VideoInfo videoInfo = new()
                {
                    Url = Url,
                    Title = video.Title,
                    Author = video.Author.ToString(),
                    Duration = video.Duration.ToString(),
                };

                return videoInfo;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                return null!;
            }
        }

        public async Task<bool> IsUrlValid(string Url)
        {
            try
            {
                Video video = await _youtubeClient.Videos.GetAsync(Url);
                return video != null;
            }
            catch (VideoUnavailableException)
            {
                return false;
            }
            catch (Exception ex)
            {

                await Console.Out.WriteLineAsync($"Error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
