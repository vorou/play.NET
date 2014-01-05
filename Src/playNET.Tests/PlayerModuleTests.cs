using System.Linq;
using FakeItEasy;
using Nancy;
using Nancy.Testing;
using playNET.App;
using playNET.Tests.Helpers;
using Ploeh.AutoFixture;
using Shouldly;

namespace playNET.Tests
{
    public class PlayerModuleTests
    {
        private readonly IFixture fixture = new Fixture();

        private static Browser CreateDefaultBrowser(IPlayer player)
        {
            return new Browser(with =>
                               {
                                   with.Module<PlayerModule>();
                                   with.Dependency(player);
                               });
        }

        public void GetRoot_Always_ReturnsHttpOK()
        {
            var sut = CreateDefaultBrowser(A.Fake<IPlayer>());

            var actual = sut.Get("/").StatusCode;

            actual.ShouldBe(HttpStatusCode.OK);
        }

        public void GetRoot_Always_ContainsTrackName()
        {
            var player = A.Fake<IPlayer>();
            A.CallTo(() => player.Status).Returns(PlaybackStatus.Playing);
            var trackName = "panda rap";
            A.CallTo(() => player.NowPlaying).Returns(trackName);
            var sut = CreateDefaultBrowser(player);

            var actual = sut.Get("/").Body.AsString();

            actual.ShouldContain(trackName);
        }

        [Input(PlaybackStatus.Playing, "Playing")]
        [Input(PlaybackStatus.Stopped, "Stopped")]
        public void GetStatus_Always_RespondsWithPlaybackStatus(PlaybackStatus status, string expected)
        {
            var player = A.Fake<IPlayer>();
            A.CallTo(() => player.Status).Returns(status);
            var sut = CreateDefaultBrowser(player);

            var actual = sut.Get("/status").Body.AsString();

            actual.ShouldBe(expected);
        }

        public void GetRoot_Always_ContainsPlaylist()
        {
            var player = A.Fake<IPlayer>();
            var tracks = fixture.CreateMany<string>(2);
            A.CallTo(() => player.Playlist).Returns(tracks);
            var sut = CreateDefaultBrowser(player);

            var actual = sut.Get("/").Body.AsString();

            actual.ShouldContain(tracks.ElementAt(0));
            actual.ShouldContain(tracks.ElementAt(1));
        }

        public void PostPlay_Always_StartsPlayback()
        {
            var player = A.Fake<IPlayer>();
            var sut = CreateDefaultBrowser(player);

            sut.Post("/play");

            A.CallTo(() => player.Play()).MustHaveHappened();
        }

        public void PostPlay_Always_ReturnsHttpOK()
        {
            var player = A.Fake<IPlayer>();
            var sut = CreateDefaultBrowser(player);

            var actual = sut.Post("/play").StatusCode;

            actual.ShouldBe(HttpStatusCode.OK);
        }

        public void PostStop_Always_StopsPlayback()
        {
            var player = A.Fake<IPlayer>();
            var sut = CreateDefaultBrowser(player);

            sut.Post("/stop");

            A.CallTo(() => player.Stop()).MustHaveHappened();
        }

        public void PostStop_Always_ReturnsHttpOK()
        {
            var player = A.Fake<IPlayer>();
            var sut = CreateDefaultBrowser(player);

            var actual = sut.Post("/stop").StatusCode;

            actual.ShouldBe(HttpStatusCode.OK);
        }
    }
}