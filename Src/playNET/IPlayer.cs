namespace playNET
{
    public interface IPlayer
    {
        PlaybackStatus Status { get; }
        void Play();
        void Stop();
    }
}