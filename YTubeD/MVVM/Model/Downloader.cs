using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTubeD.Core;
using YoutubeExplode;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace YTubeD.MVVM.Model
{
    internal class Downloader : ObservableObject
    {
        private string _outputDirectory = string.Empty;

        public YoutubeVideo Video { get; set; }
        public YoutubeClient Client { get; set; }
        public string OutputDirectory 
        {
            get { return _outputDirectory; }
            set
            {
                _outputDirectory = value;
                OnPropertyChanged();
            }
        }

        public Downloader()
        {
            Video = new YoutubeVideo();
            Client = new YoutubeClient();
        }
        public async Task<bool> IsUrlValid()
        {
            try
            {
                string oembedUrl = $"https://www.youtube.com/oembed?format=json&url={Uri.EscapeDataString(Video.Url)}";

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
        public void DownloadVideo()
        {

        }
    }
}
