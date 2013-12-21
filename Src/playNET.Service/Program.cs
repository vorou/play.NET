using System;
using Nancy.Hosting.Self;

namespace playNET.Service
{
    public class Program
    {
        private static void Main()
        {
            var baseUri = new Uri("http://localhost:666");
            using (var host = new NancyHost(baseUri))
            {
                host.Start();
                Console.Out.WriteLine("playNET is ready at {0}, press any key to stop...", baseUri);
                Console.ReadLine();
            }
        }
    }
}