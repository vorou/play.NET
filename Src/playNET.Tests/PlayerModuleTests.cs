using System.Linq;
using FakeItEasy;
using Nancy;
using Nancy.Testing;
using playNET.App;
using playNET.Tests.Helpers;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Shouldly;

namespace playNET.Tests
{
    public class PlayerModuleTests
    {
        private readonly IFixture fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());

        private Browser CreateDefaultBrowser()
        {
            return new Browser(with =>
                               {
                                   with.Module<PlayerModule>();
                                   with.Dependency(fixture.Create<IPlayer>());
                               });
        }

        public void GetRoot_Always_ReturnsHttpOK()
        {
            var sut = CreateDefaultBrowser();

            var actual = sut.Get("/").StatusCode;

            actual.ShouldBe(HttpStatusCode.OK);
        }

        public void GetRoot_Always_ContainsTrackName()
        {
            var player = fixture.Freeze<IPlayer>();
            A.CallTo(() => player.Status).Returns(PlaybackStatus.Playing);
            var trackName = "panda rap";
            A.CallTo(() => player.NowPlaying).Returns(trackName);
            var sut = CreateDefaultBrowser();

            var actual = sut.Get("/").Body.AsString();

            actual.ShouldContain(trackName);
        }

        [Input(PlaybackStatus.Playing, "Playing")]
        [Input(PlaybackStatus.Stopped, "Stopped")]
        public void GetStatus_Always_RespondsWithPlaybackStatus(PlaybackStatus status, string expected)
        {
            var player = fixture.Freeze<IPlayer>();
            A.CallTo(() => player.Status).Returns(status);
            var sut = CreateDefaultBrowser();

            var actual = sut.Get("/status").Body.AsString();

            actual.ShouldBe(expected);
        }

        public void GetRoot_Always_ContainsPlaylist()
        {
            var player = fixture.Freeze<IPlayer>();
            var tracks = fixture.CreateMany<string>(2);
            A.CallTo(() => player.Playlist).Returns(tracks);
            var sut = CreateDefaultBrowser();

            var actual = sut.Get("/").Body.AsString();

            actual.ShouldContain(tracks.ElementAt(0));
            actual.ShouldContain(tracks.ElementAt(1));
        }

        public void PostPlay_Always_StartsPlayback()
        {
            var player = fixture.Freeze<IPlayer>();
            var sut = CreateDefaultBrowser();

            sut.Post("/play");

            A.CallTo(() => player.Play()).MustHaveHappened();
        }

        public void PostStop_Always_StopsPlayback()
        {
            var player = fixture.Freeze<IPlayer>();
            var sut = CreateDefaultBrowser();

            sut.Post("/stop");

            A.CallTo(() => player.Stop()).MustHaveHappened();
        }

        [Input("/play")]
        [Input("/stop")]
        [Input("/next")]
        public void Posts_Always_ReturnsHttpOK(string uri)
        {
            var sut = CreateDefaultBrowser();

            var actual = sut.Post(uri).StatusCode;

            actual.ShouldBe(HttpStatusCode.OK);
        }

        public void PostNext_Always_CallsNextOnPlayer()
        {
            var player = fixture.Freeze<IPlayer>();
            var sut = CreateDefaultBrowser();

            sut.Post("/next");

            A.CallTo(() => player.Next()).MustHaveHappened();
        }
    }
}