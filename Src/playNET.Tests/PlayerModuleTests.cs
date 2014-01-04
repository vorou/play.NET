using FakeItEasy;
using Nancy;
using Nancy.Testing;
using playNET.App;
using playNET.Tests.Helpers;
using Shouldly;

namespace playNET.Tests
{
    public class PlayerModuleTests
    {
        private static Browser CreateDefaultBrowser(IPlayer player)
        {
            return new Browser(with =>
                               {
                                   with.Module<PlayerModule>();
                                   with.Dependency(player);
                               });
        }

        public void PlayerModule_GetToRoot_ReturnsOK()
        {
            var sut = CreateDefaultBrowser(A.Fake<IPlayer>());

            var actual = sut.Get("/").StatusCode;

            actual.ShouldBe(HttpStatusCode.OK);
        }

        public void PlayerModule_Playing_ResponseContainsTrackName()
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
        public void PlayerModule_Always_RespondsWithPlaybackStatus(PlaybackStatus status, string expected)
        {
            var player = A.Fake<IPlayer>();
            A.CallTo(() => player.Status).Returns(status);
            var sut = CreateDefaultBrowser(player);

            var actual = sut.Get("/status").Body.AsString();

            actual.ShouldBe(expected);
        }

        public void PlayerModule_PostToPlayRoute_StartsPlayback()
        {
            var player = A.Fake<IPlayer>();
            var sut = CreateDefaultBrowser(player);

            sut.Post("/play");

            A.CallTo(() => player.Play()).MustHaveHappened();
        }

        public void PlayerModule_PostToPlayRoute_Returns200()
        {
            var player = A.Fake<IPlayer>();
            var sut = CreateDefaultBrowser(player);

            var actual = sut.Post("/play").StatusCode;

            actual.ShouldBe(HttpStatusCode.OK);
        }

        public void PlayerModule_PostToStopRoute_StopsPlayback()
        {
            var player = A.Fake<IPlayer>();
            var sut = CreateDefaultBrowser(player);

            sut.Post("/stop");

            A.CallTo(() => player.Stop()).MustHaveHappened();
        }

        public void PlayerModule_PostToStopRoute_Returns200()
        {
            var player = A.Fake<IPlayer>();
            var sut = CreateDefaultBrowser(player);

            var actual = sut.Post("/stop").StatusCode;

            actual.ShouldBe(HttpStatusCode.OK);
        }
    }
}