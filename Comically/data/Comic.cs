using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Comically.data
{
    public class Comic
    {
        public ComicInfo ComicInfo { get; set; }
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
                    string[] volPaths = Directory.GetDirectories(ComicDirectory);
                    if (volPaths.Length > 0)
                    {
                        string[] imgPaths = Directory.GetFiles(volPaths[0]);
                        if (imgPaths.Length > 0)
                        {
                            return Convert.ToBase64String(File.ReadAllBytes(imgPaths[0]));
                        }
                    }

                    return "";
                }
            }
        }

        public List<Chapter> Chapters
        {
            get
            {
                return Directory.GetDirectories(ComicDirectory).Select(chapterDir =>
                {
                    return new Chapter(Directory.GetFiles(chapterDir).ToList())
                    {
                        Title = Path.GetFileName(chapterDir)
                    };
                }).ToList();
            }
        }
    }
}
