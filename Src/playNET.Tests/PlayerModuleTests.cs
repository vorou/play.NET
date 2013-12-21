using FakeItEasy;
using Nancy.Testing;
using playNET.Service;
using playNET.Tests.Helpers;
using Shouldly;

namespace playNET.Tests
{
    public class PlayerModuleTests
    {
        [Input(PlaybackStatus.Playing, "Playing")]
        [Input(PlaybackStatus.Stopped, "Stopped")]
        public void PlayerModule_Always_RespondsWithPlaybackStatus(PlaybackStatus status, string expected)
        {
            var player = A.Fake<IPlayer>();
            A.CallTo(() => player.Status).Returns(status);
            var sut = new Browser(with =>
                                  {
                                      with.Module<PlayerModule>();
                                      with.Dependency(player);
                                  });

            var actual = sut.Get("/").Body.AsString();

            actual.ShouldBe(expected);
        }
    }
}