using System.IO;
using Shouldly;

namespace playNET.Tests
{
    public class SingerTests
    {
        private const string audioDir = @"..\..\..\..\Audio";

        public void NowPlaying_NothingPlaying_ReturnsNull()
        {
            var sut = CreateSinger();
            sut.ShutUp();

            var actual = sut.NowPlaying;

            actual.ShouldBe(null);
        }

        public void NowPlaying_MediaIsPlaying_ReturnsFilenameWithoutExtension()
        {
            var sut = new Singer();
            var track = Path.Combine(audioDir, "vot-tak-vot.mp3");
            sut.Sing(new[] {track});

            var actual = sut.NowPlaying;

            actual.ShouldBe("vot-tak-vot");
        }

        private static Singer CreateSinger()
        {
            return new Singer();
        }
    }
}