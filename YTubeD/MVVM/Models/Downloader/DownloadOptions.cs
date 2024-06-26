﻿using YoutubeExplode.Videos.Streams;

namespace YTubeD.MVVM.Models.Downloader
{
    internal enum DownloadPreference
    {
        UpTo144p,
        UpTo240p,
        UpTo360p,
        UpTo480p,
        UpTo720p,
        UpTo1080p,
        UpTo1440p,
        UpTo2160p,
        AudioMp3,
    }

    internal static class DownloadPreferenceName
    {
        public static string GetName(DownloadPreference preference) =>
            preference switch
            {
                DownloadPreference.UpTo144p => "Up to 144p",
                DownloadPreference.UpTo240p => "Up to 240p",
                DownloadPreference.UpTo360p => "Up to 360p",
                DownloadPreference.UpTo480p => "Up to 480p",
                DownloadPreference.UpTo720p => "Up to 720p",
                DownloadPreference.UpTo1080p => "Up to 1080p",
                DownloadPreference.UpTo1440p => "Up to 1440p",
                DownloadPreference.UpTo2160p => "Up to 2160p",
                DownloadPreference.AudioMp3 => "Audio MP3",
                _ => throw new ArgumentOutOfRangeException(nameof(preference))
            };
    }

    internal class DownloadOption
    {
        public Container Container { get; set; }
        public IReadOnlyList<IStreamInfo> StreamInfos { get; set; }
        public DownloadPreference Preference;

        public DownloadOption(DownloadPreference preference)
        {
            Preference = preference;
            StreamInfos = new List<IStreamInfo>();
        }

        public void GetBestOption(StreamManifest streamManifest)
        {
            var videoStreamInfosMP4 = streamManifest.GetVideoOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .GroupBy(s => s.VideoQuality)
                .Select(g => g.First())
                .OrderByDescending(s => s.VideoQuality);
            var audioStreamInfo = streamManifest.GetAudioOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .GetWithHighestBitrate();
            switch (Preference)
            {
                case DownloadPreference.UpTo144p:
                    Container = Container.Mp4;
                    StreamInfos = new List<IStreamInfo>()
                    {
                        videoStreamInfosMP4
                        .Where(o => o.VideoQuality.MaxHeight <= 144)
                        .First(),
                        audioStreamInfo
                    };
                    break;
                case DownloadPreference.UpTo240p:
                    Container = Container.Mp4;
                    StreamInfos = new List<IStreamInfo>()
                    {
                        videoStreamInfosMP4
                        .Where(o => o.VideoQuality.MaxHeight <= 240)
                        .First(),
                        audioStreamInfo
                    };
                    break;
                case DownloadPreference.UpTo360p:
                    Container = Container.Mp4;
                    StreamInfos = new List<IStreamInfo>()
                    {
                        videoStreamInfosMP4
                        .Where(o => o.VideoQuality.MaxHeight <= 360)
                        .First(),
                        audioStreamInfo
                    };
                    break;
                case DownloadPreference.UpTo480p:
                    Container = Container.Mp4;
                    StreamInfos = new List<IStreamInfo>()
                    {
                        videoStreamInfosMP4
                        .Where(o => o.VideoQuality.MaxHeight <= 480)
                        .First(),
                        audioStreamInfo
                    };
                    break;
                case DownloadPreference.UpTo720p:
                    Container = Container.Mp4;
                    StreamInfos = new List<IStreamInfo>()
                    {
                        videoStreamInfosMP4
                        .Where(o => o.VideoQuality.MaxHeight <= 720)
                        .First(),
                        audioStreamInfo
                    };
                    break;
                case DownloadPreference.UpTo1080p:
                    Container = Container.Mp4;
                    StreamInfos = new List<IStreamInfo>()
                    {
                        videoStreamInfosMP4
                        .Where(o => o.VideoQuality.MaxHeight <= 1080)
                        .First(),
                        audioStreamInfo
                    };
                    break;
                case DownloadPreference.UpTo1440p:
                    Container = Container.Mp4;
                    StreamInfos = new List<IStreamInfo>()
                    {
                        videoStreamInfosMP4
                        .Where(o => o.VideoQuality.MaxHeight <= 1440)
                        .First(),
                        audioStreamInfo
                    };
                    break;
                case DownloadPreference.UpTo2160p:
                    Container = Container.Mp4;
                    StreamInfos = new List<IStreamInfo>()
                    {
                        videoStreamInfosMP4
                        .Where(o => o.VideoQuality.MaxHeight <= 2160)
                        .First(),
                        audioStreamInfo
                    };
                    break;
                case DownloadPreference.AudioMp3:
                    Container = Container.Mp3;
                    StreamInfos = new List<IStreamInfo>()
                    {
                        audioStreamInfo
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Preference));
            }
        }
    }
}
