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
        private const string TestDir = @"..\..\..\..\Audio";
        private const string TrackName = "vot-tak-vot";
        private readonly IFixture fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());

        public void NowPlaying_PlaylistEmpty_ReturnsNull()
        {
            var sut = CreatePlayerWithEmptyPlaylist();

            var actual = sut.NowPlaying;

            actual.ShouldBe(null);
        }

        public void NowPlaying_PlaylistHasTracksPlayerStopped_ReturnsNull()
        {
            var sut = CreatePlayerWithSingleTrack();

            var actual = sut.NowPlaying;

            actual.ShouldBe(null);
        }

        public void Player_WhenCreated_QueuesExistingTracks()
        {
            var sut = CreatePlayerWithSingleTrack();

            sut.Playlist.ShouldContain(TrackName);
        }

        public void Player_TrackAddedToFolder_ShouldQueueIt()
        {
            var dir = "bear";
            var filename = "panda";
            var eventArgs = new FileSystemEventArgs(WatcherChangeTypes.Created, dir, filename);
            var fileLocator = fixture.Freeze<IFileLocator>();
            var sut = fixture.Create<Player>();

            fileLocator.TrackAdded += Raise.With(eventArgs).Now;

            sut.Playlist.ShouldContain("panda");
        }

        private static Player CreatePlayerWithSingleTrack()
        {
            return new Player(new FileLocator(TestDir));
        }

        private static Player CreatePlayerWithEmptyPlaylist()
        {
            var fileLocator = A.Fake<IFileLocator>();
            A.CallTo(() => fileLocator.FindTracks()).Returns(Enumerable.Empty<string>());
            return new Player(fileLocator);
        }
    }
}