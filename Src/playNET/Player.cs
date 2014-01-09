using System.Collections.Generic;
using System.IO;

namespace playNET
{
    public class Player : IPlayer
    {
        private readonly ISinger singer;

        public Player(IFileLocator fileLocator, ISinger singer)
        {
            fileLocator.TrackAdded += FileLocatorOnTrackAdded;
            this.singer = singer;
            foreach (var track in fileLocator.FindTracks())
                singer.Queue(track);
        }

        public IEnumerable<string> Playlist
        {
            get
            {
                return singer.Playlist;
            }
        }

        public string NowPlaying
        {
            get
            {
                return singer.NowPlaying;
            }
        }

        public PlaybackStatus Status
        {
            get
            {
                return singer.NowPlaying == null ? PlaybackStatus.Stopped : PlaybackStatus.Playing;
            }
        }

        public void Stop()
        {
            singer.Stop();
        }

        public void Next()
        {
            singer.Next();
        }

        public void Play()
        {
            singer.Play();
        }

        private void FileLocatorOnTrackAdded(object sender, FileSystemEventArgs file)
        {
            singer.Queue(file.FullPath);
        }
    }
}