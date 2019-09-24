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

        public string CoverPath { get; private set; }
        public string CoverImage => Utility.FileToBase64(CoverPath);

        public List<Volume> Volumes
        {
            get
            {
                string[] vols = Directory.GetDirectories(ComicDirectory, "Volume *");
                if (vols.Length > 0)
                {
                    // if volume directories exist read them
                    vols.SortNaturally();
                    return vols.Select(volDir => new Volume(volDir)).ToList();
                }
                else
                {
                    // if not create a "virtual" volume
                    return new List<Volume>() { new Volume(ComicDirectory) };
                }
            }
        }

        public Comic(string comicDirectory)
        {
            ComicDirectory = comicDirectory;

            FindCover();
        }

        private void FindCover()
        {
            string[] coverFiles = Directory.GetFiles(ComicDirectory, "cover.*");
            if (coverFiles.Length > 0)
            {
                // get cover image path
                CoverPath = coverFiles[0];
            }
            else
            {
                // set cover path to first image of comic's path otherwise
                CoverPath = Volumes[0].CoverPath;

            }
        }
    }
}
