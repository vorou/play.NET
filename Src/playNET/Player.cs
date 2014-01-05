using System.Collections.Generic;
using System.Linq;

namespace playNET
{
    public class Player : IPlayer
    {
        private readonly IFileLocator fileLocator;
        private readonly ISinger singer;

        public Player(IFileLocator fileLocator, ISinger singer)
        {
            this.fileLocator = fileLocator;
            this.singer = singer;
        }

        public IEnumerable<string> Playlist
        {
            get
            {
                return fileLocator.FindTracks();
            }
        }

        public string NowPlaying
        {
            get
            {
                return singer.NowPlaying;
            }
        }

        public PlaybackStatus Status
        {
            get
            {
                return singer.NowPlaying == null ? PlaybackStatus.Stopped : PlaybackStatus.Playing;
            }
        }

        public void Stop()
        {
            singer.ShutUp();
        }

        public void Play()
        {
            var tracks = fileLocator.FindTracks();
            if (!tracks.Any())
                return;

            singer.Sing(tracks);
        }
    }
}