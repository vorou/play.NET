using System.Collections.Generic;
using System.IO;

namespace playNET
{
    public class Playlist : IPlaylist
    {
        private readonly string directory;

        public Playlist(string directory)
        {
            this.directory = directory;
        }

        public IEnumerable<string> GetTracks()
        {
            return Directory.GetFiles(directory, "*.mp3");
        }
    }
}