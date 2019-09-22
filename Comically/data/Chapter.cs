using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comically.data
{
    public class Chapter
    {
        private List<string> PagePaths { get; set; }

        public string Title { get; set; }

        public Chapter(List<string> pagePaths)
        {
            this.PagePaths = pagePaths;
        }

        public uint PageCount()
        {
            return (uint)PagePaths.Count;
        }

        public string GetPage(uint index)
        {
            return Convert.ToBase64String(File.ReadAllBytes(PagePaths[(int)index]));
        }
    }
}
