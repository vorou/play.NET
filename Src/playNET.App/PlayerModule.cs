using Nancy;

namespace playNET.MVC
{
    public class PlayerModule : NancyModule
    {
        public PlayerModule(IPlayer player)
        {
            Get["/"] = _ => View["Index", player.NowPlaying];

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