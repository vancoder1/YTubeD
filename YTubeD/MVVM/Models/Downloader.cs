using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Policy;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Converter;
using YTubeD.Core;
using System.Drawing.Drawing2D;

namespace YTubeD.MVVM.Model
{
    internal class Downloader : ObservableObject
    {
        private string _outputDirectory = string.Empty;
        private string _url = string.Empty;

        public Video YoutubeVideo { get; set; }
        public YoutubeClient Client { get; set; }
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }
        public string OutputDirectory 
        {
            get => _outputDirectory;
            set
            {
                _outputDirectory = value;
                OnPropertyChanged();
            }
        }

        public Downloader()
        {
            Client = new YoutubeClient();
        }

        public async Task<bool> IsUrlValid()
        {
            try
            {
                string oembedUrl = $"https://www.youtube.com/oembed?format=json&url={Uri.EscapeDataString(Url)}";

                using (var httpclient = new HttpClient())
                {
                    HttpResponseMessage response = await httpclient.GetAsync(oembedUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject oembedData = JObject.Parse(responseBody);

                    // Check if the response contains the video title
                    if (oembedData["title"] != null)
                    {
                        return true; // Video exists
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
            return false; // URL is not valid or video does not exist
        }

        public async Task<IEnumerable<IStreamInfo>> FetchInfo()
        {
            var streamManifest = await Client.Videos.Streams.GetManifestAsync(Url);
            var videoStreamInfos = streamManifest.GetVideoOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .GroupBy(s => s.VideoQuality)
                .Select(g => g.First())
                .OrderByDescending(s => s.VideoQuality);
            var audioStreamInfo = streamManifest.GetAudioOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .GetWithHighestBitrate();

            var combinedStreams = new List<IStreamInfo>();
            combinedStreams.AddRange(videoStreamInfos);
            combinedStreams.Add(audioStreamInfo);

            YoutubeVideo = await Client.Videos.GetAsync(Url);

            return combinedStreams;
        }

        public async Task Download(IStreamInfo quality)
        {
            if (quality == null)
            {
                throw new ArgumentNullException();
            }
            
            string sanitizedTitle = string.Join("_", YoutubeVideo.Title.Split(Path.GetInvalidFileNameChars()));
            string outputFilePath = Path.Combine(OutputDirectory, $"{sanitizedTitle}.{quality.Container}");
            var stream = await Client.Videos.Streams.GetManifestAsync(Url);
            if (quality is IVideoStreamInfo)
            {
                var audioStreamInfo = stream
                    .GetAudioOnlyStreams()
                    .Where(s => s.Container == quality.Container)
                    .GetWithHighestBitrate();
                var streamInfos = new IStreamInfo[] { audioStreamInfo, quality };
                await Client.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(outputFilePath)
                .Build());
            }
            else
            {
                await Client.Videos.Streams.DownloadAsync(quality, outputFilePath);
            }
            
        }
    }
}
