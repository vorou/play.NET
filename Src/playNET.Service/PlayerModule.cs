using Nancy;

namespace playNET.Service
{
    public class PlayerModule : NancyModule
    {
        public PlayerModule()
        {
            Get["/"] = _ => "Stopped";
        }
    }
}