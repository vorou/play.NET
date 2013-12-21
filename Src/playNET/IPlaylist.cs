using System.Collections.Generic;

namespace playNET
{
    public interface IPlaylist
    {
        IEnumerable<string> GetTracks();
    }
}