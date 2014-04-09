using System.Collections.Generic;
using System.IO;
using WMPLib;

namespace playNET
{
    public class Player : IPlayer
    {
        private const int VolumeStep = 2;
        private readonly WindowsMediaPlayer wmp;

        public Player(IFileLocator fileLocator)
        {
            wmp = new WindowsMediaPlayer();
            fileLocator.TrackAdded += FileLocatorOnTrackAdded;
            foreach (var track in fileLocator.FindTracks())
                Queue(track);
        }

        public void Stop()
        {
            wmp.controls.stop();
        }

        public string NowPlaying
        {
            get
            {
                if (wmp.playState != WMPPlayState.wmppsPlaying)
                    return null;

                var currentMedia = wmp.currentMedia;
                if (currentMedia == null)
                    return null;

                return currentMedia.getItemInfo("Title");
            }
        }

        public IEnumerable<string> Playlist
        {
            get
            {
                for (var i = 0; i < wmp.currentPlaylist.count; i++)
                    yield return wmp.currentPlaylist.Item[i].name;
            }
        }

        private void Queue(string track)
        {
            wmp.currentPlaylist.appendItem(wmp.newMedia(Path.GetFullPath(track)));
        }

        public void Play()
        {
            wmp.controls.play();
        }

        public void Next()
        {
            wmp.controls.next();
        }

        public void VolumeDown()
        {
            wmp.settings.volume -= VolumeStep;
        }

        public void VolumeUp()
        {
            wmp.settings.volume += VolumeStep;
        }

        public void Previous()
        {
            wmp.controls.previous();
        }

        private void FileLocatorOnTrackAdded(object sender, FileSystemEventArgs file)
        {
            Queue(file.FullPath);
        }
    }
}