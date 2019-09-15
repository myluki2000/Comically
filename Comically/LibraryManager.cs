using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comically.data;

namespace Comically
{
    public class LibraryManager
    {
        private const string LIBRARY_PATH = "E:\\Manga_Lib";

        public static List<Comic> GetComics()
        {
            return Directory.GetDirectories(LIBRARY_PATH).Select(x => new Comic() {Name = x, ComicDirectory = Path.Combine(LIBRARY_PATH, x)}).ToList();
        }
    }
}
