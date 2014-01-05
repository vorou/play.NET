using System.Linq;

namespace playNET
{
    public class Player : IPlayer
    {
        private readonly IPlaylist playlist;
        private readonly ISinger singer;

        public Player(ISinger singer, IPlaylist playlist)
        {
            this.singer = singer;
            this.playlist = playlist;
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
            var tracks = playlist.GetTracks();
            if (!tracks.Any())
                return;

            singer.Sing(tracks);
        }
    }
}