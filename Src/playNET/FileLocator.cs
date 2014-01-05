using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace playNET
{
    public class FileLocator : IFileLocator
    {
        private readonly string directory;

        public FileLocator(string directory)
        {
            if (!Directory.EnumerateFiles(directory, "*.mp3").Any())
                throw new FileNotFoundException();
            this.directory = directory;
        }

        public IEnumerable<string> FindTracks()
        {
            return Directory.GetFiles(directory, "*.mp3");
        }
    }
}