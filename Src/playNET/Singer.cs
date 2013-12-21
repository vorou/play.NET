using System;
using NAudio.Wave;

namespace playNET
{
    public class Singer : ISinger
    {
        private static readonly ISinger instance = new Singer();
        private readonly IWavePlayer waveOutDevice;
        private WaveStream mainOutputStream;
        private WaveChannel32 volumeStream;

        private Singer()
        {
            waveOutDevice = new WaveOut();
        }

        public static ISinger Instance
        {
            get
            {
                return instance;
            }
        }

        public void Sing(string track)
        {
            mainOutputStream = CreateInputStream(track);
            waveOutDevice.Init(mainOutputStream);
            waveOutDevice.Play();
        }

        public void ShutUp()
        {
            CloseWaveOut();
        }

        private WaveStream CreateInputStream(string fileName)
        {
            WaveChannel32 inputStream;
            if (fileName.EndsWith(".mp3"))
            {
                WaveStream mp3Reader = new Mp3FileReader(fileName);
                inputStream = new WaveChannel32(mp3Reader);
            }
            else
                throw new InvalidOperationException("Unsupported extension");
            volumeStream = inputStream;
            return volumeStream;
        }

        private void CloseWaveOut()
        {
            if (waveOutDevice != null)
                waveOutDevice.Stop();
            if (mainOutputStream != null)
            {
                volumeStream.Close();
                mainOutputStream.Close();
            }
            if (waveOutDevice != null)
                waveOutDevice.Dispose();
        }
    }
}