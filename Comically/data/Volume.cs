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
                string[] chapterPaths = Directory.GetDirectories(volPath);
                chapterPaths.SortNaturally();
                return chapterPaths.Select(chapterDir => new Chapter(Directory.GetFiles(chapterDir).ToList())
                {
                    Title = Path.GetFileName(chapterDir)
                }).ToList();
            }
        }

        public string CoverPath { get; private set; }
        public string CoverImage => Utility.FileToBase64(CoverPath);

        private readonly string volPath;
        public Volume(string path)
        {
            volPath = path;
            Title = Path.GetFileName(path);

            FindCover();
        }

        private void FindCover()
        {
            CoverPath = Directory.GetFiles(Directory.GetDirectories(volPath)[0])[0];
        }
    }
}
