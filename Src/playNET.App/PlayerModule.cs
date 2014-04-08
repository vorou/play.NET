using Nancy;

namespace playNET.App
{
    public class PlayerModule : NancyModule
    {
        public PlayerModule(IPlayer player)
        {
            Get["/"] = _ => View["Index", new IndexViewModel {NowPlaying = player.NowPlaying, Playlist = player.Playlist}];

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

            Post["/next"] = _ =>
                            {
                                player.Next();
                                return HttpStatusCode.OK;
                            };

            Post["/voldown"] = _ =>
                             {
                                 player.VolumeDown();
                                 return HttpStatusCode.OK;
                             };
        }
    }
}