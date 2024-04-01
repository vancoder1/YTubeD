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
using YTubeD.MVVM.Models.Downloader;

namespace YTubeD.MVVM.Model
{
    internal class Downloader : ObservableObject
    {
        public YoutubeClient Client { get; set; }
        public Downloader()
        {
            Client = new YoutubeClient();
        }

        public async Task<bool> IsUrlValid(string Url)
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
                        return true;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
            return false;
        }

        public async Task<IEnumerable<IStreamInfo>> FetchInfo(string Url)
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

            Video youtubeVideo = await Client.Videos.GetAsync(Url);

            return combinedStreams;
        }

        public async Task Download(string Url, IStreamInfo quality, VideoInfo video, string outputDirectory)
        {
            if (quality == null)
            {
                throw new ArgumentNullException();
            }

            YoutubeClient client = new YoutubeClient();
            string sanitizedTitle = string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars()));
            string outputFilePath = Path.Combine(outputDirectory, $"{sanitizedTitle}.{quality.Container}");
            var stream = await client.Videos.Streams.GetManifestAsync(Url);
            if (quality is IVideoStreamInfo)
            {
                var audioStreamInfo = stream
                    .GetAudioOnlyStreams()
                    .Where(s => s.Container == quality.Container)
                    .GetWithHighestBitrate();
                var streamInfos = new IStreamInfo[] { audioStreamInfo, quality };
                await client.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(outputFilePath)
                .Build());
            }
            else
            {
                await client.Videos.Streams.DownloadAsync(quality, outputFilePath);
            }
            
        }
    }
}
