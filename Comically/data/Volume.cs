using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comically.data
{
    public class Volume
    {
        public string Title { get; set; }
        public List<Chapter> Chapters
        {
            get
            {
                return Directory.GetDirectories(volPath).Select(chapterDir =>
                {
                    return new Chapter(Directory.GetFiles(chapterDir).ToList())
                    {
                        Title = Path.GetFileName(chapterDir)
                    };
                }).ToList();
            }
        }

        private readonly string volPath;
        public Volume(string path)
        {
            volPath = path;
            Title = Path.GetFileName(path);
        }
    }
}
