using System.Linq;

namespace playNET
{
    public class Player : IPlayer
    {
        private readonly ISinger singer;

        public Player(ISinger singer)
        {
            this.singer = singer;
            Status = PlaybackStatus.Stopped;
        }

        public PlaybackStatus Status { get; private set; }

        public void Play(IPlaylist playlist)
        {
            var tracks = playlist.GetTracks();
            if (!tracks.Any())
                return;

            var track = tracks.FirstOrDefault();
            singer.Sing(track);
            Status = PlaybackStatus.Playing;
        }

        public void Stop()
        {
            singer.ShutUp();
            Status = PlaybackStatus.Stopped;
        }
    }
}