using System.Collections.Generic;

namespace playNET
{
    public interface IPlayer
    {
        string NowPlaying { get; }
        IEnumerable<string> Playlist { get; }
        void Queue(string track);
        void Play();
        void Stop();
        void Next();
    }
}