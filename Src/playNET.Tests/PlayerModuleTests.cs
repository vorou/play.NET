using Nancy.Testing;
using playNET.Service;
using Shouldly;

namespace playNET.Tests
{
    public class PlayerModuleTests
    {
        public void PlayerModule_ByDefault_StatusIsStopped()
        {
            var sut = new Browser(with => with.Module<PlayerModule>());

            var actual = sut.Get("/").Body.AsString();

            actual.ShouldBe("Stopped");
        }
    }
}