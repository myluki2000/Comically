using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Comically
{
    public class MainModule : Nancy.NancyModule
    {
        public MainModule()
        {
            Get("/", p => View["index.cshtml", LibraryManager.GetComics()]);
        }
    }
}
