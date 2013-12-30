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
            Status = PlaybackStatus.Stopped;
        }

        public PlaybackStatus Status { get; private set; }
        public string NowPlaying { get; private set; }

        public void Stop()
        {
            singer.ShutUp();
            Status = PlaybackStatus.Stopped;
        }

        public void Play()
        {
            var tracks = playlist.GetTracks();
            if (!tracks.Any())
                return;

            singer.Sing(tracks);
            Status = PlaybackStatus.Playing;
        }
    }
}