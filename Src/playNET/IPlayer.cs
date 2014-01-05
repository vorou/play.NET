using System.Collections.Generic;

namespace playNET
{
    public interface IPlayer
    {
        PlaybackStatus Status { get; }
        string NowPlaying { get; }
        IEnumerable<string> Playlist { get; }
        void Play();
        void Stop();
    }
}