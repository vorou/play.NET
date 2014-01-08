using System.Collections.Generic;
using System.IO;

namespace playNET
{
    public class FileLocator : IFileLocator
    {
        private static IFileLocator instance;
        private readonly FileSystemWatcher watcher;

        public FileLocator(string directory)
        {
            watcher = new FileSystemWatcher(directory) {EnableRaisingEvents = true};
        }

        public IEnumerable<string> FindTracks()
        {
            return Directory.GetFiles(watcher.Path, "*.mp3");
        }

        public event FileSystemEventHandler TrackAdded
        {
            add
            {
                watcher.Created += value;
            }
            remove
            {
            }
        }
    }
}