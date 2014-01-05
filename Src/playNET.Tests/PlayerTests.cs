using System.IO;
using FakeItEasy;
using playNET.Tests.Helpers;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Shouldly;

namespace playNET.Tests
{
    public class PlayerTests
    {
        private readonly IFixture fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());

        [Input("panda", PlaybackStatus.Playing)]
        [Input(null, PlaybackStatus.Stopped)]
        public void Status_Always_CalculatedFromNowPlaying(string nowPlaying, PlaybackStatus expected)
        {
            var singer = fixture.Freeze<ISinger>();
            var sut = fixture.Create<Player>();
            A.CallTo(() => singer.NowPlaying).Returns(nowPlaying);

            var actual = sut.Status;

            actual.ShouldBe(expected);
        }

        public void Play_Always_SendsTracksFromPlaylistToSinger()
        {
            var singer = fixture.Freeze<ISinger>();
            var playlist = fixture.Freeze<IPlaylist>();
            var tracks = new[] {Path.GetRandomFileName()};
            A.CallTo(() => playlist.GetTracks()).Returns(tracks);
            var sut = fixture.Freeze<Player>();

            sut.Play();

            A.CallTo(() => singer.Sing(tracks)).MustHaveHappened();
        }

        public void Stop_Always_StopsPlayback()
        {
            var singer = fixture.Freeze<ISinger>();
            var sut = fixture.Create<Player>();

            sut.Stop();

            A.CallTo(() => singer.ShutUp()).MustHaveHappened();
        }

        public void NowPlaying_Always_AsksSingerWhatIsPlaying()
        {
            var singer = fixture.Freeze<ISinger>();
            var sut = fixture.Create<Player>();
            var nowPlaying = fixture.Create<string>();
            A.CallTo(() => singer.NowPlaying).Returns(nowPlaying);

            var actual = sut.NowPlaying;

            actual.ShouldBe(nowPlaying);
        }

        public void Playlist_Always_ReturnsInternalPlaylist()
        {
            var playlist = fixture.Freeze<IPlaylist>();
            var sut = fixture.Create<Player>();

            var actual = sut.Playlist;

            actual.ShouldBeSameAs(playlist);
        }
    }
}