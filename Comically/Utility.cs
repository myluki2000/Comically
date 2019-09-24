using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Comically
{
    class Utility
    {
        public static string FileToBase64(string path)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(path));
        }
    }
}
