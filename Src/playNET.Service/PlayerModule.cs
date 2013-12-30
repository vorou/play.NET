﻿using Nancy;

namespace playNET.Service
{
    public class PlayerModule : NancyModule
    {
        public PlayerModule(IPlayer player)
        {
            Get["/"] = _ => View["index.html"];

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