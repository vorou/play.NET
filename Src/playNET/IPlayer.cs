namespace playNET
{
    public interface IPlayer
    {
        PlaybackStatus Status { get; }
        string NowPlaying { get; }
        void Play();
        void Stop();
    }
}