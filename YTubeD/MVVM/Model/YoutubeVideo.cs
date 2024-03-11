using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTubeD.Core;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YTubeD.MVVM.Model
{
    internal class YoutubeVideo : ObservableObject
    {
        private string _url = string.Empty;
        private string _title = string.Empty;
        private string _author = string.Empty;
        private string _duration = string.Empty;
        private IEnumerable<IAudioStreamInfo> _audioQualities;
        private IEnumerable<IVideoStreamInfo> _videoQualities;

        public string Url 
        { 
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }
        public string Title 
        { 
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string Author 
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }
        public string Duration 
        { 
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<IAudioStreamInfo> AudioQualities
        {
            get { return _audioQualities; }
            set
            {
                _audioQualities = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<IVideoStreamInfo> VideoQualities
        {
            get { return _videoQualities; }
            set
            {
                _videoQualities = value;
                OnPropertyChanged();
            }
        }

        public YoutubeVideo()
        {
            AudioQualities = new List<IAudioStreamInfo>();
            VideoQualities = new List<IVideoStreamInfo>();
        }

        public async Task Method()
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync("u_yIGGhubZs");
            var audioStreamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();
            var videoStreamInfo = streamManifest.GetVideoStreams().First(s => s.VideoQuality.Label == "1080p60");
        }
    }
}
