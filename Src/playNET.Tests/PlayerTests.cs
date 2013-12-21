using System.IO;
using System.Linq;
using FakeItEasy;
using Shouldly;

namespace playNET.Tests
{
    public class PlayerTests
    {
        private static Player CreatePlayerWithEmptyPlaylist()
        {
            var playlist = A.Fake<IPlaylist>();
            A.CallTo(() => playlist.GetTracks()).Returns(Enumerable.Empty<string>());
            return new Player(A.Fake<ISinger>(), playlist);
        }

        private static Player CreatePlayingPlayer(ISinger singer)
        {
            var playlist = A.Fake<IPlaylist>();
            A.CallTo(() => playlist.GetTracks()).Returns(new[] {Path.GetRandomFileName()});
            var sut = new Player(singer, playlist);
            sut.Play();
            return sut;
        }

        public void Player_ByDefault_Stopped()
        {
            var sut = CreatePlayerWithEmptyPlaylist();

            var actual = sut.Status;

            actual.ShouldBe(PlaybackStatus.Stopped);
        }

        public void Play_NoTracks_RemainsStopped()
        {
            var sut = CreatePlayerWithEmptyPlaylist();

            sut.Play();

            sut.Status.ShouldBe(PlaybackStatus.Stopped);
        }

        public void Play_OneTrack_PlaysIt()
        {
            var singer = A.Fake<ISinger>();
            var playlist = A.Fake<IPlaylist>();
            var track = Path.GetRandomFileName();
            A.CallTo(() => playlist.GetTracks()).Returns(new[] {track});
            var sut = new Player(singer, playlist);

            sut.Play();

            A.CallTo(() => singer.Sing(track)).MustHaveHappened();
        }

        public void Play_TracksExists_StatusChangesToPlaying()
        {
            var playlist = A.Fake<IPlaylist>();
            A.CallTo(() => playlist.GetTracks()).Returns(new[] {Path.GetRandomFileName()});
            var sut = new Player(A.Fake<ISinger>(),playlist);

            sut.Play();

            sut.Status.ShouldBe(PlaybackStatus.Playing);
        }

        public void Stop_WasPlaying_StopsPlayback()
        {
            var singer = A.Fake<ISinger>();
            var sut = CreatePlayingPlayer(singer);

            sut.Stop();

            A.CallTo(() => singer.ShutUp()).MustHaveHappened();
        }

        public void Stop_WasPlaying_StatusChangesToStopped()
        {
            var singer = A.Fake<ISinger>();
            var sut = CreatePlayingPlayer(singer);

            sut.Stop();

            sut.Status.ShouldBe(PlaybackStatus.Stopped);
        }
    }
}