using System.IO;
using Nancy;
using Nancy.TinyIoc;

namespace playNET.Service
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(Singer.Instance);
            container.Register<IPlaylist>(new Playlist(Path.GetTempPath()));
        }
    }
}