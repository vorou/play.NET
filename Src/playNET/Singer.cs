using System.Collections.Generic;
using System.IO;
using WMPLib;

namespace playNET
{
    /// <summary>
    /// Talks to a real player.
    /// </summary>
    public class Singer : ISinger
    {
        private readonly WindowsMediaPlayer wmp;

        public Singer()
        {
            wmp = new WindowsMediaPlayer();
        }

        public void ShutUp()
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

        public void Queue(string track)
        {
            wmp.currentPlaylist.appendItem(wmp.newMedia(Path.GetFullPath(track)));
        }

        public void Sing()
        {
            wmp.controls.play();
        }
    }
}