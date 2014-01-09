using System.Collections.Generic;

namespace playNET
{
    public interface ISinger
    {
        void Stop();
        string NowPlaying { get; }
        IEnumerable<string> Playlist { get; }
        void Queue(string track);
        void Play();
        void Next();
    }
}