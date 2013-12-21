using FakeItEasy;
using Nancy.Testing;
using playNET.Service;
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

        [Input(PlaybackStatus.Playing, "Playing")]
        [Input(PlaybackStatus.Stopped, "Stopped")]
        public void PlayerModule_Always_RespondsWithPlaybackStatus(PlaybackStatus status, string expected)
        {
            var player = A.Fake<IPlayer>();
            A.CallTo(() => player.Status).Returns(status);
            var sut = CreateDefaultBrowser(player);

            var actual = sut.Get("/").Body.AsString();

            actual.ShouldBe(expected);
        }

        public void PlayerModule_PostToPlayRoute_StartsPlayback()
        {
            var player = A.Fake<IPlayer>();
            var sut = CreateDefaultBrowser(player);

            sut.Post("/play");

            A.CallTo(() => player.Play(A<IPlaylist>.Ignored)).MustHaveHappened();
        }
    }
}