using System;
using System.IO;
using System.Threading;
using Shouldly;

namespace playNET.Tests
{
    public class FileLocatorTests : IDisposable
    {
        private readonly string targetDirectory;

        public FileLocatorTests()
        {
            targetDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(targetDirectory);
        }

        public void Dispose()
        {
            if (Directory.Exists(targetDirectory))
                Directory.Delete(targetDirectory, true);
        }

        public void FindTracks_NoMp3sInDir_ReturnsEmpty()
        {
            var sut = CreateFileLocator(targetDirectory);

            var actual = sut.FindTracks();

            actual.ShouldBeEmpty();
        }

        public void FindTracks_OneMp3File_ReturnsIt()
        {
            var pathToMp3 = CreateEmptyFile(targetDirectory, ".mp3");
            var sut = CreateFileLocator(targetDirectory);

            var actual = sut.FindTracks();

            actual.ShouldContain(pathToMp3);
        }

        public void FindTracks_FileWithUnknownExtension_IgnoresIt()
        {
            CreateEmptyFile(targetDirectory, ".mp3");
            var pathToUnknownFile = CreateEmptyFile(targetDirectory, ".xyz");
            var sut = CreateFileLocator(targetDirectory);

            var actual = sut.FindTracks();

            actual.ShouldNotContain(pathToUnknownFile);
        }

        public void FileLocator_NewMp3Created_RaisesNewTrack()
        {
            var pathToMp3 = CreateRandomFilePath(targetDirectory, ".mp3");
            var sut = CreateFileLocator(targetDirectory);
            var signal = new ManualResetEventSlim();
            sut.TrackAdded += (sender, args) =>
                              {
                                  args.FullPath.ShouldBe(pathToMp3);
                                  signal.Set();
                              };

            CreateEmptyFile(pathToMp3);

            signal.Wait(500);
            if(!signal.IsSet)
                throw new Exception("Timeout");
            //todo: wait for reply on https://github.com/plioi/fixie/issues/32
        }

        public void FileLocator_Always_IgnoresNonMp3Files()
        {
            var pathToMp3 = CreateRandomFilePath(targetDirectory, ".xyz");
            var sut = CreateFileLocator(targetDirectory);
            var signal = new ManualResetEventSlim();
            sut.TrackAdded += (sender, args) => signal.Set();

            CreateEmptyFile(pathToMp3);

            signal.Wait(500);
            signal.IsSet.ShouldBe(false);
            //todo: wait for reply on https://github.com/plioi/fixie/issues/32
        }

        private static IFileLocator CreateFileLocator(string directory)
        {
            return new FileLocator(directory);
        }

        private static string CreateEmptyFile(string targetDirectory, string extension)
        {
            var path = CreateRandomFilePath(targetDirectory, extension);
            CreateEmptyFile(path);
            return path;
        }

        private static void CreateEmptyFile(string path)
        {
            File.WriteAllBytes(path, new byte[0]);
        }

        private static string CreateRandomFilePath(string targetDirectory, string extension)
        {
            return Path.Combine(targetDirectory, Path.ChangeExtension(Path.GetRandomFileName(), extension));
        }
    }
}