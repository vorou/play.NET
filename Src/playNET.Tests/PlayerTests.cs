using System.IO;
using System.Linq;
using FakeItEasy;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Shouldly;

namespace playNET.Tests
{
    public class PlayerTests
    {
        private readonly IFixture fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());

        private Player CreatePlayerWithEmptyPlaylist()
        {
            var playlist = fixture.Freeze<IPlaylist>();
            A.CallTo(() => playlist.GetTracks()).Returns(Enumerable.Empty<string>());
            return fixture.Create<Player>();
        }

        private Player CreatePlayingPlayer()
        {
            var playlist = fixture.Freeze<IPlaylist>();
            A.CallTo(() => playlist.GetTracks()).Returns(new[] {Path.GetRandomFileName()});
            var sut = fixture.Create<Player>();

            sut.Play();

            return sut;
        }

        public void Player_ByDefault_Stopped()
        {
            var sut = fixture.Create<Player>();

            var actual = sut.Status;

            actual.ShouldBe(PlaybackStatus.Stopped);
        }

        public void Play_NoTracksInPlaylist_RemainsStopped()
        {
            var sut = CreatePlayerWithEmptyPlaylist();

            sut.Play();

            sut.Status.ShouldBe(PlaybackStatus.Stopped);
        }

        public void Play_PlaylistHasTracks_PlaysIt()
        {
            var singer = fixture.Freeze<ISinger>();
            var playlist = fixture.Freeze<IPlaylist>();
            var tracks = new[] {Path.GetRandomFileName()};
            A.CallTo(() => playlist.GetTracks()).Returns(tracks);
            var sut = fixture.Freeze<Player>();

            sut.Play();

            A.CallTo(() => singer.Sing(tracks)).MustHaveHappened();
        }

        public void Play_PlaylistHasTracks_StatusChangesToPlaying()
        {
            var playlist = fixture.Freeze<IPlaylist>();
            A.CallTo(() => playlist.GetTracks()).Returns(fixture.CreateMany<string>());
            var sut = fixture.Create<Player>();

            sut.Play();

            sut.Status.ShouldBe(PlaybackStatus.Playing);
        }

        public void Stop_WasPlaying_StopsPlayback()
        {
            var singer = fixture.Freeze<ISinger>();
            var sut = CreatePlayingPlayer();

            sut.Stop();

            A.CallTo(() => singer.ShutUp()).MustHaveHappened();
        }

        public void Stop_WasPlaying_StatusChangesToStopped()
        {
            var sut = CreatePlayingPlayer();

            sut.Stop();

            sut.Status.ShouldBe(PlaybackStatus.Stopped);
        }

        public void NowPlaying_PlayerStopped_ReturnsNull()
        {
            var sut = fixture.Create<Player>();

            sut.NowPlaying.ShouldBe(null);
        }
    }
}