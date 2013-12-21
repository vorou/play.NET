using System.Collections.Generic;
using System.Linq;
using WMPLib;

namespace playNET
{
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
            var medias = tracks.Select(t => wmp.newMedia(t));
            foreach (var media in medias)
                wmp.currentPlaylist.appendItem(media);
            wmp.controls.play();
        }

        public void ShutUp()
        {
            wmp.controls.stop();
        }
    }
}