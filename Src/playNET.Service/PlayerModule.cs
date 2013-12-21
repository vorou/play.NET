﻿using Nancy;

namespace playNET.Service
{
    public class PlayerModule : NancyModule
    {
        public PlayerModule(IPlayer player)
        {
            Get["/"] = _ => player.Status.ToString();

            Post["/play"] = _ =>
                            {
                                player.Play(null);
                                return HttpStatusCode.OK;
                            };
        }
    }
}