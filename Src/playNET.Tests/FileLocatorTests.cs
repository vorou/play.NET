﻿using System;
using System.IO;
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

        private static string CreateEmptyFile(string targetDirectory, string extension)
        {
            var path = Path.Combine(targetDirectory, Path.ChangeExtension(Path.GetRandomFileName(), extension));
            File.WriteAllBytes(path, new byte[0]);
            return path;
        }

        public void Playlist_NoMp3sInDir_ThrowsInCtor()
        {
            Should.Throw<FileNotFoundException>(() => new FileLocator(targetDirectory));
        }

        public void Playlist_OneMp3File_ReturnsIt()
        {
            var pathToMp3 = CreateEmptyFile(targetDirectory, ".mp3");
            var sut = new FileLocator(targetDirectory);

            var actual = sut.FindTracks();

            actual.ShouldContain(pathToMp3);
        }

        public void Playlist_FileWithUnknownExtension_IgnoresIt()
        {
            CreateEmptyFile(targetDirectory, ".mp3");
            var pathToUnknownFile = CreateEmptyFile(targetDirectory, ".xyz");
            var sut = new FileLocator(targetDirectory);

            var actual = sut.FindTracks();

            actual.ShouldNotContain(pathToUnknownFile);
        }
    }
}