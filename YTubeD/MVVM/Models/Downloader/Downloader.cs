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
using System.IO.Packaging;
using YTubeD.MVVM.Models;
using AngleSharp.Dom;
using YoutubeExplode.Exceptions;

namespace YTubeD.MVVM.Model
{
    internal class Downloader : ObservableObject
    {
        public YoutubeClient Client { get; set; }
        private SettingsModel Settings { get; set; }

        public Downloader()
        {
            Client = new YoutubeClient();
            Settings = new SettingsModel();
            Settings.LoadSettings();
        }

        public async Task<bool> IsUrlValid(string Url)
        {
            try
            {
                var video = await Client.Videos.GetAsync(Url);
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

        public async Task Download(VideoInfo videoInfo, DownloadOption quality)
        {
            if (quality == null)
            {
                throw new ArgumentNullException();
            }
            var stream = await Client.Videos.Streams.GetManifestAsync(videoInfo.Url);
            quality.GetBestOption(stream);
            string sanitizedTitle = string.Join("_", videoInfo.Title.Split(Path.GetInvalidFileNameChars()));
            string outputFilePath = Path.Combine(Settings.SavingPath, $"{sanitizedTitle}.{quality.Container.Name}");        
            await Client.Videos.DownloadAsync(quality.StreamInfos, new ConversionRequestBuilder(outputFilePath)
                .SetContainer(quality.Container)
                .SetPreset(ConversionPreset.Medium)
                .Build());
        }
    }
}
