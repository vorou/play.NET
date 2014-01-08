using System.Collections.Generic;
using System.IO;
using System.Linq;
using WMPLib;

namespace playNET
{
    /// <summary>
    /// Talks to a real player.
    /// </summary>
    public class Singer : ISinger
    {
        private static readonly ISinger instance = new Singer();
        private readonly WindowsMediaPlayer wmp;

        private Singer()
        {
            wmp = new WindowsMediaPlayer();
        }

        public static ISinger Instance
        {
            get
            {
                return instance;
            }
        }

        public void Sing(IEnumerable<string> tracks)
        {
            var medias = tracks.Select(Path.GetFullPath).Select(t => wmp.newMedia(t));
            foreach (var media in medias)
                wmp.currentPlaylist.appendItem(media);
            wmp.controls.play();
        }

        public void ShutUp()
        {
            wmp.controls.stop();
        }

        public string NowPlaying
        {
            get
            {
                var currentMedia = wmp.currentMedia;
                if (currentMedia == null)
                    return null;

                return currentMedia.getItemInfo("Title");
            }
        }

        public void Queue(string track)
        {
        }
    }
}