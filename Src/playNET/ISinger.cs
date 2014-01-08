using System.Collections.Generic;

namespace playNET
{
    public interface ISinger
    {
        void Sing(IEnumerable<string> tracks);
        void ShutUp();
        string NowPlaying { get; }
        void Queue(string track);
    }
}