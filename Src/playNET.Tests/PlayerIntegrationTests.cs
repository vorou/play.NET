using System.IO;
using FakeItEasy;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Shouldly;

namespace playNET.Tests
{
    public class PlayerIntegrationTests
    {
        private const string TestDir = @"..\..\..\..\Audio";
        private const string TrackName = "vot-tak-vot";
        private readonly IFixture fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        private readonly string trackPath = Path.Combine(TestDir, "vot-tak-vot.mp3");

        public void NowPlaying_PlaylistEmpty_ReturnsNull()
        {
            var sut = CreatePlayer();

            var actual = sut.NowPlaying;

            actual.ShouldBe(null);
        }

        public void NowPlaying_PlaylistHasTracksPlayerStopped_ReturnsNull()
        {
            var sut = CreatePlayer();
            sut.Queue(trackPath);

            var actual = sut.NowPlaying;

            actual.ShouldBe(null);
        }

        public void Queueing_TrackQueued_ItsInPlaylist()
        {
            var sut = CreatePlayer();

            sut.Queue(trackPath);

            sut.Playlist.ShouldContain(TrackName);
        }

        public void Player_WhenCreated_QueuesExistingTracks()
        {
            var sut = CreatePlayer();

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

        private static Player CreatePlayer()
        {
            return new Player(new FileLocator(TestDir));
        }
    }
}