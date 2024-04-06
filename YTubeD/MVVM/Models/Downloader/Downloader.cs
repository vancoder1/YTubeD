using System.IO;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YTubeD.Core;
using YTubeD.MVVM.Models;
using YTubeD.MVVM.Models.Downloader;

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
        }

        public async Task Download(VideoInfo videoInfo, DownloadOption quality)
        {
            ArgumentNullException.ThrowIfNull(quality);
            Settings.LoadSettings();
            var stream = await Client.Videos.Streams.GetManifestAsync(videoInfo.Url);
            quality.GetBestOption(stream);
            string sanitizedTitle = string.Join("_", videoInfo.Title.Split(Path.GetInvalidFileNameChars()));
            string outputFilePath = Path.Combine(Settings.SavingPath, $"{sanitizedTitle}.{quality.Container.Name}");
            await Client.Videos.DownloadAsync(quality.StreamInfos, new ConversionRequestBuilder(outputFilePath)
                .SetContainer(quality.Container)
                .SetPreset(ConversionPreset.UltraFast)
                .Build());
        }
    }
}