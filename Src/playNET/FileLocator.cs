﻿using System.Collections.Generic;
using System.IO;

namespace playNET
{
    public class FileLocator : IFileLocator
    {
        private readonly FileSystemWatcher watcher;

        public FileLocator(string directory)
        {
            watcher = new FileSystemWatcher(directory, "*.mp3") {EnableRaisingEvents = true};
        }

        public IEnumerable<string> FindTracks()
        {
            var tracks = Directory.GetFiles(watcher.Path, "*.mp3");
            return tracks;
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