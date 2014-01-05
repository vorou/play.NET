using System.IO;
using Shouldly;

namespace playNET.Tests
{
    public class SingerTests
    {
        private const string audioDir = @"..\..\..\..\Audio";

        public void NowPlaying_NothingPlaying_ReturnsNull()
        {
            Singer.Instance.ShutUp();

            var actual = Singer.Instance.NowPlaying;

            actual.ShouldBe(null);
        }

        public void NowPlaying_MediaIsPlaying_ReturnsFilenameWithoutExtension()
        {
            var track = Path.Combine(audioDir, "vot-tak-vot.mp3");
            Singer.Instance.Sing(new[] {track});

            var actual = Singer.Instance.NowPlaying;

            actual.ShouldBe("vot-tak-vot");
        }
    }
}