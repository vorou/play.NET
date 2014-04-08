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
            var trackName = "panda rap";
            A.CallTo(() => player.NowPlaying).Returns(trackName);
            var sut = CreateDefaultBrowser();

            var actual = sut.Get("/").Body.AsString();

            actual.ShouldContain(trackName);
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
        [Input("/voldown")]
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

        public void PostVoldown_Always_CallsVolumeDownOnPlayer()
        {
            var player = fixture.Freeze<IPlayer>();
            var sut = CreateDefaultBrowser();

            sut.Post("/voldown");

            A.CallTo(() => player.VolumeDown()).MustHaveHappened();
        }

        public void PostVolup_Always_CallsVolumeUpOnPlayer()
        {
            var player = fixture.Freeze<IPlayer>();
            var sut = CreateDefaultBrowser();

            sut.Post("/volup");

            A.CallTo(() => player.VolumeUp()).MustHaveHappened();
        }

        public void GetNowPlaying_Always_ReturnsNameFromPlayer()
        {
            var player = fixture.Freeze<IPlayer>();
            A.CallTo(() => player.NowPlaying).Returns("panda");
            var sut = CreateDefaultBrowser();

            var actual = sut.Get("/now");

            actual.Body.AsString().ShouldBe("panda");
        }
    }
}