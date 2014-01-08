using System.Collections.Generic;
using System.IO;

namespace playNET
{
    public interface IFileLocator
    {
        IEnumerable<string> FindTracks();
        event FileSystemEventHandler TrackAdded;
    }
}