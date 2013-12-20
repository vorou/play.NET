using Shouldly;

namespace playNET.Tests
{
    public class PlayerTests
    {
        public void Player_ByDefault_StatusIsStopped()
        {
            var sut = new Player();

            var actual = sut.Status;

            actual.ShouldBe(PlaybackStatus.Stopped);
        }
    }

    public enum PlaybackStatus
    {
        Stopped
    }

    public class Player
    {
        public PlaybackStatus Status { get; private set; }
    }
}