using System.Collections.Generic;

namespace playNET
{
    public interface ISinger
    {
        void ShutUp();
        string NowPlaying { get; }
        IEnumerable<string> Playlist { get; }
        void Queue(string track);
        void Sing();
        void Next();
    }
}