using System.Collections.Generic;

namespace playNET
{
    public interface IPlayer
    {
        string NowPlaying { get; }
        IEnumerable<string> Playlist { get; }
        void Play();
        void Stop();
        void Next();
        void VolumeDown();
        void VolumeUp();
        void Previous();
    }
}