using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace playNET
{
    public class Playlist : IPlaylist
    {
        private readonly string directory;

        public Playlist(string directory)
        {
            if (!Directory.EnumerateFiles(directory, "*.mp3").Any())
                throw new FileNotFoundException();
            this.directory = directory;
        }

        public IEnumerable<string> GetTracks()
        {
            return Directory.GetFiles(directory, "*.mp3");
        }
    }
}