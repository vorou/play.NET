using Nancy;

namespace playNET.App
{
    public class PlayerModule : NancyModule
    {
        public PlayerModule(IPlayer player)
        {
            Get["/"] = _ => View["Index", new IndexViewModel {NowPlaying = player.NowPlaying, Playlist = player.Playlist}];

            Get["/status"] = _ => player.Status.ToString();

            Post["/play"] = _ =>
                            {
                                player.Play();
                                return HttpStatusCode.OK;
                            };

            Post["/stop"] = _ =>
                            {
                                player.Stop();
                                return HttpStatusCode.OK;
                            };
        }
    }
}