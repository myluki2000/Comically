using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Comically.data;
using Nancy.ViewEngines.Razor;

namespace Comically
{
    public class MainModule : Nancy.NancyModule
    {
        public MainModule()
        {
            Get("/", p => View["index.cshtml", LibraryManager.GetComics()]);
            Get("/info/{id}", p =>
            {
                Comic c = LibraryManager.GetComicById(uint.Parse(p["id"]));
                return View["info.cshtml", c];
            });
        }
    }
}
