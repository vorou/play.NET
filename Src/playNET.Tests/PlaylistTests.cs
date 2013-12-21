using System;
using System.IO;
using Shouldly;

namespace playNET.Tests
{
    public class PlaylistTests : IDisposable
    {
        private readonly string targetDirectory;

        public PlaylistTests()
        {
            targetDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(targetDirectory);
        }

        public void Dispose()
        {
            if (Directory.Exists(targetDirectory))
                Directory.Delete(targetDirectory, true);
        }

        private static void CreateEmptyFile(string path)
        {
            File.WriteAllBytes(path, new byte[0]);
        }

        public void Playlist_DirectoryEmpty_ReturnsNoTracks()
        {
            var sut = new Playlist(targetDirectory);

            var actual = sut.GetTracks();

            actual.ShouldBeEmpty();
        }

        public void Playlist_OneMp3File_ReturnsIt()
        {
            var pathToMp3 = Path.Combine(targetDirectory, Path.ChangeExtension(Path.GetRandomFileName(), ".mp3"));
            CreateEmptyFile(pathToMp3);
            var sut = new Playlist(targetDirectory);

            var actual = sut.GetTracks();

            actual.ShouldContain(pathToMp3);
        }

        public void Playlist_FileWithUnknownExtension_IgnoresIt()
        {
            var pathToUnknownFile = Path.Combine(targetDirectory, Path.GetRandomFileName());
            CreateEmptyFile(pathToUnknownFile);
            var sut = new Playlist(targetDirectory);

            var actual = sut.GetTracks();

            actual.ShouldNotContain(pathToUnknownFile);
        }
    }
}