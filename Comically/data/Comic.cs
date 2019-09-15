using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comically.data
{
    public class Comic
    {
        public string Name { get; set; }
        public string ComicDirectory { get; set; }

        public string CoverImage
        {
            get
            {
                string[] coverFiles = Directory.GetFiles(ComicDirectory, "cover.*");
                if (coverFiles.Length > 0)
                {
                    // return cover image in comic directory if it exists
                    return Convert.ToBase64String(File.ReadAllBytes(coverFiles[0]));
                }
                else
                {
                    // return first image of comic otherwise
                    return "";
                }
            }
        }

        public List<Chapter> Chapters { get; set; }
    }
}
