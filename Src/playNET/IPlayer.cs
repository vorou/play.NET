namespace playNET
{
    public interface IPlayer
    {
        PlaybackStatus Status { get; }
        void Play(IPlaylist playlist);
        void Stop();
    }
}