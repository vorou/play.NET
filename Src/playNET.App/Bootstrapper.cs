﻿using System.IO;
using Nancy;
using Nancy.TinyIoc;

namespace playNET.App
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(Singer.Instance);
            container.Register<IFileLocator>(new FileLocator(Path.GetTempPath())).AsSingleton();
        }
    }
}