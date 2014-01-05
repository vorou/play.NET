using System.Collections.Generic;

namespace playNET.App
{
    public class IndexViewModel
    {
        public string NowPlaying { get; set; }
        public IEnumerable<string> Playlist { get; set; }
    }
}