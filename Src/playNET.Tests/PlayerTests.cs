using System.IO;
using System.Linq;
using FakeItEasy;
using playNET.Tests.Helpers;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Shouldly;

namespace playNET.Tests
{
    public class PlayerTests
    {
        private readonly IFixture fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());

        [Input("panda", PlaybackStatus.Playing)]
        [Input(null, PlaybackStatus.Stopped)]
        public void Status_Always_CalculatedFromNowPlaying(string nowPlaying, PlaybackStatus expected)
        {
            var singer = fixture.Freeze<ISinger>();
            var sut = fixture.Create<Player>();
            A.CallTo(() => singer.NowPlaying).Returns(nowPlaying);

            var actual = sut.Status;

            actual.ShouldBe(expected);
        }

        public void Play_TracksExists_SendsToSinger()
        {
            var fileLocator = fixture.Freeze<IFileLocator>();
            var tracks = fixture.CreateMany<string>();
            A.CallTo(() => fileLocator.FindTracks()).Returns(tracks);
            var singer = fixture.Freeze<ISinger>();
            var sut = fixture.Create<Player>();

            sut.Play();

            A.CallTo(() => singer.Sing(tracks)).MustHaveHappened();
        }

        public void Play_NoTracks_DoesntCallSinger()
        {
            var fileLocator = fixture.Freeze<IFileLocator>();
            A.CallTo(() => fileLocator.FindTracks()).Returns(Enumerable.Empty<string>());
            var singer = fixture.Freeze<ISinger>();
            var sut = fixture.Create<Player>();

            sut.Play();

            A.CallTo(singer).MustNotHaveHappened();
        }

        public void Stop_Always_StopsPlayback()
        {
            var singer = fixture.Freeze<ISinger>();
            var sut = fixture.Create<Player>();

            sut.Stop();

            A.CallTo(() => singer.ShutUp()).MustHaveHappened();
        }

        public void NowPlaying_Always_AsksSingerWhatIsPlaying()
        {
            var singer = fixture.Freeze<ISinger>();
            var sut = fixture.Create<Player>();
            var nowPlaying = fixture.Create<string>();
            A.CallTo(() => singer.NowPlaying).Returns(nowPlaying);

            var actual = sut.NowPlaying;

            actual.ShouldBe(nowPlaying);
        }

        public void Playlist_Always_LeavesOnlyFileNames()
        {
            var fileLocator = fixture.Freeze<IFileLocator>();
            var fullPath = @"C:\bears\panda.xyz";
            var tracks = new[] {fullPath};
            A.CallTo(() => fileLocator.FindTracks()).Returns(tracks);
            var sut = fixture.Create<Player>();

            var actual = sut.Playlist;

            actual.ShouldContain("panda");
        }

        public void Player_TrackAdded_ShouldQueueIt()
        {
            var dir = @"a:\";
            var filename = "b";
            var fileLocator = fixture.Freeze<IFileLocator>();
            var singer = fixture.Freeze<ISinger>();
            var sut = fixture.Create<Player>();

            fileLocator.TrackAdded += Raise.With(new FileSystemEventArgs(WatcherChangeTypes.Created, dir, filename)).Now;

            var expected = @"a:\b";
            A.CallTo(() => singer.Queue(expected)).MustHaveHappened();
        }
    }
}