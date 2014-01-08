using System.Collections.Generic;

namespace playNET
{
    public interface ISinger
    {
        void Sing(IEnumerable<string> tracks);
        void ShutUp();
        string NowPlaying { get; }
        IEnumerable<string> Playlist { get; }
        void Queue(string track);
    }
}