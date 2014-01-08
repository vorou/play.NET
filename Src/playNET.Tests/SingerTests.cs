using System.IO;
using Shouldly;

namespace playNET.Tests
{
    public class SingerTests
    {
        private const string AudioDir = @"..\..\..\..\Audio";
        private const string TestTrackFileName = "vot-tak-vot.mp3";
        private const string TestTrackFileNameWithoutExtension = "vot-tak-vot";
        private string TestTrack
        {
            get
            {
                return Path.Combine(AudioDir, TestTrackFileName);
            }
        }

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
            sut.Queue(TestTrack);

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

            sut.Queue(TestTrack);

            sut.Playlist.ShouldContain(TestTrackFileNameWithoutExtension);
        }

        private static Singer CreateSinger()
        {
            return new Singer();
        }
    }
}