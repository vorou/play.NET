using System.IO;
using Shouldly;

namespace playNET.Tests
{
    public class SingerTests
    {
        private const string AudioDir = @"..\..\..\..\Audio";

        public void NowPlaying_PlaylistEmpty_ReturnsNull()
        {
            var sut = CreateSinger();
            sut.ShutUp();

            var actual = sut.NowPlaying;

            actual.ShouldBe(null);
        }

        public void NowPlaying_PlaylistHasTracksPlayerStopped_ReturnsNull()
        {
            var sut = CreateSinger();
            sut.Queue(Path.Combine(AudioDir, "vot-tak-vot.mp3"));

            var actual = sut.NowPlaying;

            actual.ShouldBe(null);
        }

        public void Playlist_ByDefault_Empty()
        {
            var sut = CreateSinger();

            var actual = sut.Playlist;

            actual.ShouldBeEmpty();
        }

        public void Queueing_TrackWasQueued_ItsInPlaylist()
        {
            var sut = CreateSinger();

            sut.Queue(Path.Combine(AudioDir, "vot-tak-vot.mp3"));

            sut.Playlist.ShouldContain("vot-tak-vot");
        }

        private static Singer CreateSinger()
        {
            return new Singer();
        }
    }
}