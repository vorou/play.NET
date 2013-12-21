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

        public void Sing(string track)
        {
            wmp.URL = track;
            wmp.controls.play();
        }

        public void ShutUp()
        {
            wmp.controls.stop();
        }
    }
}