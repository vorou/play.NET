﻿using System.Configuration;
using System.IO;
using Nancy;
using Nancy.TinyIoc;

namespace playNET.App
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<Player>().AsSingleton();
            container.Register<IFileLocator>(new FileLocator(ConfigurationManager.AppSettings["mediapath"] ?? Path.GetTempPath()));
        }
    }
}