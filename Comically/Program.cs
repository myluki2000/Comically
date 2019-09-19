using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.ViewEngines.SuperSimpleViewEngine;

namespace Comically
{
    class Program
    {
        private const string SERVER_URL = "http://localhost:4269";

        private static readonly HostConfiguration hostConfig = new HostConfiguration()
        {
            UrlReservations = new UrlReservations() {CreateAutomatically = true}
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Beginning library scan...");
            LibraryManager.ScanLibrary();
            Console.WriteLine("Library scan complete.");


            using (NancyHost host = new NancyHost(new Uri(SERVER_URL), new DefaultNancyBootstrapper(), hostConfig))
            {
                host.Start();
                Console.WriteLine("Starting server on " + SERVER_URL);
                Console.ReadLine();
            }
        }
    }
}
